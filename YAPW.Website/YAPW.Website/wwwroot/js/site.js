"use strict";

// Global Variables
//
var currentAjaxRequest;
var xhrPool = [];

// Push all ajax request to the xhrPool, all the pending requests in the pool will be cancelled when user clicks on a new page from the menu
//
$.ajaxSetup({
    cache: false,
    type: 'async',
    beforeSend: function (jqXHR) {
        xhrPool.push(jqXHR);
    }
});

//$(document).ready(function () {
//    //$(document).ajaxStart(function () { Pace.restart(); });
//});

function CancelAllAjaxRequest() {
    // Abort any pending ajax requests
    $(xhrPool).each(function (i, jqXHR) {
        jqXHR.abort();
    });
    xhrPool = [];
}

$(document).on('click', '.customRefreshButton', function (e) {
    e.preventDefault();

    let functionality = $(this).attr("data-functionality");

    if (!IsNullOrEmptyOrWhiteSpace(functionality) && functionality === 'refresh') {

        let refreshContainer = $(this).attr("data-refresh-container");
        let sourceUrl = $(this).attr("data-refresh-source");

        if (IsNullOrEmptyOrWhiteSpace(sourceUrl)) {
            throw new DOMException("Refresh url data source is missing", "Missing Url");
        }

        let updateHistory = $(this).attr("data-update-history");
        let useOverlay = $(this).attr("data-overlay");

        if (useOverlay === "true") {
            //NEED TO FIX
            //APPEND A NEW OVERLAY AFTER EACH RELOAD
            $(".overlay").remove();
            let overlay = '<div class="overlay"><i class="fas fa-2x fa-sync-alt fa-spin"></i></div>';
            $(refreshContainer).append(overlay);
            $(".overlay").insertAfter(refreshContainer);
        }

        let historyUrl = !IsNullOrEmptyOrWhiteSpace(updateHistory) && updateHistory === "true" ? sourceUrl : "";

        if (!IsNullOrEmptyOrWhiteSpace(historyUrl)) {
            UpdateBrowserHistory(historyUrl);
        }

        GetAjaxContent(sourceUrl, refreshContainer, ".overlay", updateHistory, historyUrl);
    }
});

$(document).on('click', '.deleteLink', function (e) {
    e.preventDefault();

    let afterSuccessUrl = $(this).attr("data-delete-success-url");
    let requestUrl = $(this).attr("href");
    let requestType = $(this).attr("data-delete-verb");
    let loadingArea = $(this).attr("data-delete-area");

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: requestUrl,
                cache: false,
                data: "",
                type: requestType,
                success: function (response) {
                    Swal.fire(
                        'Deleted!',
                        response,
                        'success'
                    );

                    GetAjaxContent(afterSuccessUrl, loadingArea, ".overlay", false);
                },
                error: function (xhr, textStatus, errorThrown) {
                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: xhr.responseJSON["message"],
                    });
                }
            });
        }
    });
    $(".swal2-container").css('z-index', 2147483510);
});

async function LoadAjaxPage(verb, url, updateHistory) {
    currentAjaxRequest = $.ajax({
        url: url,
        method: verb,
        data: {},
        cache: false,
        success: async function (data, responseCode, jqxhr) {
            $('#mainContentArea').html(data).promise().done(async function () {
                HandleAutoClick();
                if ($(window).width() < 768) {
                    $('[data-widget="pushmenu"]').PushMenu('collapse');
                }

                currentAjaxRequest = null;
            });

            // Abort any pending ajax requests and update browser history
            CancelAllAjaxRequest();

            if (updateHistory) {
                UpdateBrowserHistory(url);
            }
        }
    });
}

function HandleAutoClick() {
    //$('[data-autoClick="true"]').trigger("click");
    setTimeout(function () {
        $('[data-autoClick="true"]').trigger("click");
    }, 100);
    console.log($('[data-autoClick="true"]'));
}

async function InitialPageLoad() {
    //alert("okkkkkk");
    var currentUrl = window.location.hash.substr(1);
    //alert(currentUrl);
    if (currentUrl !== "") {
        CancelAllAjaxRequest();
        window.history.pushState(currentUrl, null, "" + currentUrl);
        await LoadAjaxPage('GET', currentUrl);
        //$('#mainContentArea').load(currentUrl);
    }
}

//$(document).on('click', '.', async function (e) {
//    e.preventDefault();
//    await LoadAjaxPage('GET', $(this).attr('href'), true);
//});

function UpdateBrowserHistory(requestUrl) {
    var pageUrl = requestUrl.split('')[0];
    var hashUrl = pageUrl.length > 1 ? '' + pageUrl.substring(1) : "";

    if (!IsNullOrEmptyOrWhiteSpace(hashUrl)) {
        window.history.pushState(pageUrl, null, hashUrl);
    } else {
        window.history.pushState(pageUrl, null, pageUrl);
    }
}

// This will handle the back and forward buttons navigation since we are using hash urls
//
$(window).on('popstate', async function () {
    var eventState = event.state;
    if (eventState === null) {
        await LoadAjaxPage('GET', document.location.origin);
    } else {
        await LoadAjaxPage('GET', eventState);
    }

    $('.modal-backdrop').fadeOut();
    $('.iziModal-button-close').trigger('click');
});

async function GetAjaxData(url) {
    return await (await fetch(url)).json();
}

async function GenerateDataTable(table, retrieve, initialPageLength, stateSave, allowExport) {
    return $(table).DataTable({
        "stateSave": stateSave,
        "pageLength": initialPageLength,
        //"responsive": true,
        "retrieve": retrieve
    });
}

async function InstantiateDataTable() {
    let table = $(".customDataTable");
    if (table.length > 0 && table.attr("data-dataTable") == 'true') {
        let tableElement = $(table);
        let retrieve = tableElement.attr("data-dataTable-retrieve") ?? false;
        let initialPageLength = tableElement.attr("data-dataTable-page-length") ?? 10;
        let stateSave = tableElement.attr("data-dataTable-state-save") ?? false;

        await GenerateDataTable(table, retrieve, initialPageLength, stateSave);
    }
}

async function InstantiateDataTableInModal(targetModal) {
    let table = $(targetModal).find(".customDataTable");

    if (table.length > 0) {
        let tableElement = $(table);
        let retrieve = tableElement.attr("data-dataTable-retrieve") ?? false;
        let initialPageLength = tableElement.attr("data-dataTable-page-length") ?? 10;
        let stateSave = tableElement.attr("data-dataTable-state-save") ?? false;

        await GenerateDataTable(table, retrieve, initialPageLength, stateSave);
    }
}

function InstantiateMagicSuggest(field, placeHolder, method, dataSource, maxSelection, selectionPosition, valueField, submitButton, displayField, queryParamName, disableSubmitIfInvalid, selectionContainer) {

    var query = queryParamName !== null ? queryParamName : 'query';
    var ms = $(field).magicSuggest({
        allowFreeEntries: false,
        selectionContainer: selectionContainer,
        placeholder: placeHolder,
        selectionStacked: true,
        selectionPosition: selectionPosition,
        useTabKey: true,
        expandOnFocus: false,
        highlight: false,
        maxDropHeight: 160,
        maxSelection: maxSelection,
        minChars: 1,
        displayField: displayField,
        hideTrigger: true,
        allowDuplicates: false,
        useCommaKey: true,
        method: method,
        cls: 'form-control-lg',
        data: dataSource,
        noSuggestionText: 'No result matching the term {{query}}',
        required: true,
        selectFirst: true,
        typeDelay: 15,
        valueField: valueField,
        queryParam: query
    });
    if (disableSubmitIfInvalid) {
        $(ms).on('selectionchange', function (e, m) {
            var valuesCount = this.getValue();

            if (valuesCount.length > 0) {
                $(submitButton).prop("disabled", false);
            } else {
                $(submitButton).prop("disabled", true);
            }
        });
    }

    return ms;
}

function postData(url = '', data = {}) {
    // Default options are marked with *
    const response = fetch(url, {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
            //'Content-Type': 'application/x-www-form-urlencoded'
        },
        redirect: 'follow', // manual, *follow, error
        referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: JSON.stringify(data) // body data type must match "Content-Type" header
    });
    return response.json(); // parses JSON response into native JavaScript objects
}

async function GetOperationsByHour(date, zoneId, operation) {
    var requestUrl = `operations/json/count/byHour?operation=${operation}&date=${date}&zone=${zoneId}`;
    return await GetAjaxData(requestUrl);
}

function GenerateApexChartDonut(label, data, id, type, title, formatter = "") {
    var options = {
        chart: {
            width: "100%",
            type: type
        },
        series: data,
        labels: label,
        colors: ['#e07a5f', '#3d405b', '#F6B079', '#b5838d', '#81b29a', '#f2cc8f', '#457b9d',
            '#4a4e69', '#c9ada7', '#83c5be', '#8d99ae'],
        plotOptions: {
            pie: {
                donut: {
                    labels: {
                        show: true,
                        name: {
                            show: true,
                            fontSize: '22px',
                            fontFamily: 'Rubik',
                            color: '#dfsda',
                            offsetY: -10
                        },
                        value: {
                            show: true,
                            fontSize: '16px',
                            fontFamily: 'Helvetica, Arial, sans-serif',
                            color: undefined,
                            offsetY: 16,
                            formatter: function (val) {
                                if (formatter == "$") {
                                    return new Intl.NumberFormat('en-US', {
                                        style: 'currency',
                                        currency: 'USD'
                                    }).format(val);
                                }
                                return formatter + val;
                            }
                        },
                        total: {
                            show: true,
                            label: 'Total',
                            color: '#373d3f',
                            formatter: function (w) {
                                if (formatter == "$") {
                                    var c = w.globals.seriesTotals.reduce((a, b) => {
                                        return a + b
                                    }, 0)
                                    return new Intl.NumberFormat('en-US', {
                                        style: 'currency',
                                        currency: 'USD'
                                    }).format(c);
                                }
                                return formatter + w.globals.seriesTotals.reduce((a, b) => {
                                    return a + b
                                }, 0)
                            }
                        }
                    }
                }
            }
        },
        tooltip: {
            enabled: true,
            y: {
                formatter: function (val) {
                    if (formatter == "$") {
                        return new Intl.NumberFormat('en-US', {style: 'currency', currency: 'USD'}).format(val);
                    }
                    return formatter + val;
                },
                title: {
                    formatter: function (seriesName) {
                        return ''
                    }
                }
            }
        },
        dataLabels: {
            formatter: function (val, opts) {
                if (formatter == "$") {
                    return new Intl.NumberFormat('en-US', {
                        style: 'currency',
                        currency: 'USD'
                    }).format(opts.w.config.series[opts.seriesIndex]);
                }
                return formatter + opts.w.config.series[opts.seriesIndex];
            },
        },
        title: {
            text: title,
            style: {
                fontSize: '18px'
            }
        }
    };

    var chart = new ApexCharts(document.querySelector(id), options);
    chart.render();
}

function GenerateApexChartRadialBar(label, data, id, color) {
    var options = {
        series: data,
        chart: {
            height: 200,
            type: "radialBar",
            offsetY: -10,
        },
        colors: [color],
        plotOptions: {
            radialBar: {
                dataLabels: {
                    name: {
                        offsetY: 20,
                        color: "#22223b",
                        formatter: function () {
                            return [label];
                        }
                    },
                    value: {
                        formatter: function (val) {
                            return parseFloat(val);
                        },
                        color: "#4a4e69",
                        offsetY: -30,
                        fontSize: "22px"
                    }
                }
            }
        },
    };
    var chart = new ApexCharts(document.querySelector(id), options);
    chart.render();
}

function GenerateApexChart(label, data, id, type, title, isHorizontal, isStacked = false, showLegend, headerCategory, headerValue, formatter = "", height = "400px") {
    var options = {
        chart: {
            height: height,
            type: type,
            stacked: isStacked,
            toolbar: {
                export: {
                    csv: {
                        headerCategory: headerCategory,
                        headerValue: headerValue,
                        dateFormatter(timestamp) {
                            return new Date(timestamp).toDateString()
                        }
                    }
                },
            },
        },
        series: data,
        labels: label,
        stroke: {
            curve: 'straight'
        },
        xaxis: {
            categories: label,
            labels: {
                formatter: function (val) {
                    return val;
                }
            },
        },
        yaxis: {
            labels: {
                formatter: function (val) {
                    if (formatter == "$") {
                        return new Intl.NumberFormat('en-US', {style: 'currency', currency: 'USD'}).format(val);
                    }
                    return formatter + val;
                }
            },
        },
        colors: ['#e07a5f', '#3d405b', '#f4a261', '#b5838d', '#81b29a', '#f2cc8f', '#457b9d',
            '#4a4e69', '#c9ada7', '#ee6c4d', '#8d99ae'],
        plotOptions: {
            bar: {
                horizontal: isHorizontal,
                columnWidth: '40px',
                distributed: true
            },
        },
        title: {
            text: title,
            style: {
                fontSize: '18px'
            }
        },
        dataLabels: {
            show: true,
            name: {
                offsetY: -10,
                show: true,
                color: '#888',
                fontSize: '17px'
            },
            value: {
                formatter: function (val) {
                    return val;
                },
                color: '#111',
                fontSize: '36px',
                show: true,
            },
            formatter: function (val) {
                return val;
            },
        },
        animations: {
            enabled: true,
            easing: 'easeinout',
            speed: 100,
            animateGradually: {
                enabled: true,
                delay: 150
            },
            dynamicAnimation: {
                enabled: true,
                speed: 150
            }
        },
        legend: {
            show: showLegend
        },
        tooltip: {
            enabled: true,
            shared: true,
            intersect: false,
        }
    }
    var chart = new ApexCharts(document.querySelector(id), options);
    chart.render();
}

function GetGreetingMessage() {
    var now = new Date();
    var currentHour = now.getHours();
    var msg;
    if (currentHour < 12)
        msg = 'Good Morning';
    else if (currentHour == 12)
        msg = 'Good Noon';
    else if (currentHour >= 12 && currentHour <= 17)
        msg = 'Good Afternoon';
    else if (currentHour >= 17 && currentHour <= 24)
        msg = 'Good Evening';
    return msg;
}

$(document).on('click', '.showIziModal', async function (e) {
    e.stopPropagation();
    e.preventDefault();

    let id = IsNullOrEmptyOrWhiteSpace($(this).attr("data-ID")) ? "" : $(this).attr("data-ID");
    let targetModal = $(this).attr('data-izimodal-open');
    let refreshUrl = $(this).attr('href');
    let topMargin = $(this).attr('data-izimodal-top');
    let zindex = $(this).attr('data-izimodal-zindex');
    let showRefreshButton = $(this).attr('data-izimodal-refresh') === "true";
    let width = IsNullOrEmptyOrWhiteSpace($(this).attr('data-izimodal-width')) ? 900 : $(this).attr('data-izimodal-width');
    let color = $(this).attr('data-izimodal-color');
    let modalTitle = IsNullOrEmptyOrWhiteSpace($(this).attr('data-izimodal-title')) ? "" : $(this).attr('data-izimodal-title') + id;
    let modalSubTitle = IsNullOrEmptyOrWhiteSpace($(this).attr('data-izimodal-subtitle')) ? "" : $(this).attr('data-izimodal-subtitle') + id;
    let allowFullScreen = $(this).attr('data-izimodal-fullScreenBUtton') === "true";

    // Create the div container of the modal on the fly and remove it on modal close to prevent caching
    await TriggerModal('#mainBodyContent', targetModal, refreshUrl, width, color, topMargin, modalTitle, allowFullScreen, zindex, showRefreshButton, modalSubTitle);
})

//izi Modal Handeling
async function TriggerModal(bodyId, targetModal, refreshUrl, width, color, topMargin, modalTitle, allowFullScreen, zindex, showRefreshButton, modalSubTitle) {

    var refreshButtonString = '<button class="ModalRefreshButton" style="Display:none;"><i class="fas fa-sync-alt"></i></button>';

    $(bodyId).append('<div id="' + targetModal.substring(1) + '" data-iziModal-preventClose="true"><div class="iziModalContent"></div></div>');

    var mm = $(targetModal).iziModal({
        title: modalTitle,
        overlayColor: 'rgba(0,0,0,0.8)',
        width: width,
        top: parseInt(topMargin),
        headerColor: color,
        transitionIn: 'fadeInRight',
        transitionOut: 'fadeOutDown',
        bodyOverflow: true,
        overlayClose: false,
        radius: 8,
        zindex: zindex,
        history: false,
        closeOnEscape: true,
        fullscreen: allowFullScreen,
        onOpening: async function (targetModal) {
            $(targetModal).iziModal('startLoading');
        },
        afterRender: async function () {

            $('body').css('overflow', 'hidden');

            await RequestModalContent(targetModal, refreshUrl);

            if (showRefreshButton) {

                $(targetModal).children(".iziModal-header").children(".iziModal-header-buttons").append(refreshButtonString);

                var closeButton = $(targetModal).children(".iziModal-header").children(".iziModal-header-buttons").children('.iziModal-button-close');
                var refreshButton = $(targetModal).children(".iziModal-header").children(".iziModal-header-buttons").children(".ModalRefreshButton");

                $(refreshButton).show(350);

                if (!allowFullScreen) {
                    refreshButton.css("right", "23px");
                }

                $(refreshButton).on('click', function (ev) {
                    ev.preventDefault();
                    $(this).children("i").addClass("fa-spin");
                    $(this).prop('disabled', true);
                    $(closeButton).prop('disabled', true);
                    $(targetModal).children(".iziModal-header").children(".modDate").remove();
                    $(targetModal).iziModal('startLoading');
                    $.ajax({
                        url: refreshUrl,
                        method: 'GET',
                        data: {},
                        cache: false,
                        success: async function (data, textStatus, xhr) {
                            $(targetModal + ' .iziModalContent').html(data).promise().done(async function () {
                                $(targetModal).iziModal('stopLoading');
                                await InstantiateDataTableInModal(targetModal);
                            });
                        },
                        complete: function (xhr, textStatus) {
                            $(refreshButton).children("i").removeClass("fa-spin");
                            $(refreshButton).prop('disabled', false);
                            $(closeButton).prop('disabled', false);
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: xhr.responseJSON.message,
                            });
                        }
                    });
                });
            }

            if (!IsNullOrEmptyOrWhiteSpace(modalSubTitle)) {
                var modalTitle = $(document).find(targetModal + ' .iziModal-header-title');
                var modalSubtitleDiv = '<div class="iziModalSubtitle">' + modalSubTitle + '</div>';

                $(targetModal).append(modalSubtitleDiv);

                $('.iziModalSubtitle').insertAfter(modalTitle);
            }
        },
        onOpened: async function () {

        },
        onClosed: function () {
            document.querySelectorAll(targetModal).forEach(modal => {
                modal.remove();
            });

            $(targetModal).iziModal('destroy');
            if ($('.iziModal').length === 0) {
                $('body').css('overflow', 'auto');
            }
        }
    });
}

function PrepareAndPostForm(formElement, errorContainer, submitButton, postUrl, modalToClose, modalOrigin, afterSuccessLoadArea, postProcessAction) {

    var afterSuccessUrl = modalOrigin === null || modalOrigin === "" || modalOrigin === undefined ? $("#RequestOrigin").val() : modalOrigin;
    var loadingArea = IsNullOrEmptyOrWhiteSpace(afterSuccessLoadArea) ? "#mainContentArea" : afterSuccessLoadArea

    //$(formElement + " input:text").first().focus();
    $(formElement).on('submit', function (e) {
        e.preventDefault();

        var form = $(this);

        $(errorContainer).html("");
        if (form.valid()) {
            $(submitButton).button("loading");
            $(submitButton).attr('disabled', true);

            $.ajax({
                type: "POST",
                url: postUrl,
                data: form.serialize(),
                success: function (response) {
                    const Toast = Swal.mixin({
                        toast: true,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 4000,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    })
                    Toast.fire({
                        icon: 'success',
                        title: "Created"
                    })
                    if (!IsNullOrEmptyOrWhiteSpace(postProcessAction)) {
                        postProcessAction();
                    }
                    if (!IsNullOrEmptyOrWhiteSpace(afterSuccessUrl)) {
                        GetAjaxContent(afterSuccessUrl, loadingArea, ".overlay", false);
                    }
                    //$('.ModalRefreshButton').trigger('click');
                    $(modalToClose).iziModal('close');

                },
                failure: function (response) {
                    alert("failed" + response);
                },
                error: function (xhr, textStatus, errorThrown) {
                    $(errorContainer).append(xhr.responseText + '<br />');
                    $(formElement + " input:text").first().focus();
                    $(submitButton).attr('disabled', false);
                }
            }).always(function () {
                $(submitButton).button("reset");
            });
        }
    });
}

function PrepareAndPostFormWithoutRefresh(formElement, errorContainer, submitButton, postUrl, modalToClose, modalOrigin, afterSuccessLoadArea, overlay, ocontainer) {

    var afterSuccessUrl = modalOrigin === null || modalOrigin === "" || modalOrigin === undefined ? $("#RequestOrigin").val() : modalOrigin;
    var loadingArea = IsNullOrEmptyOrWhiteSpace(afterSuccessLoadArea) ? "#mainContentArea" : afterSuccessLoadArea
    $(formElement).on('submit', function (e) {
        var form = $(this);

        $(errorContainer).html("");
        if (form.valid()) {
            //$(submitButton).button("loading");
            $(overlay).remove();
            let overlayy = '<div class="overlay"><i class="fas fa-2x fa-sync-alt fa-spin"></i></div>';
            $(ocontainer).append(overlayy);
            $(overlay).insertAfter(ocontainer);
            $(submitButton).attr('disabled', true);

            e.preventDefault();
            $(afterSuccessLoadArea).html("");
            $.ajax({
                type: "POST",
                url: postUrl,
                data: form.serialize(),
                success: function (response) {
                    $(afterSuccessLoadArea).html(response);
                },
                failure: function (response) {
                    alert("failed" + response);
                },
                error: function (xhr, textStatus, errorThrown) {
                    $(errorContainer).append(xhr.responseText + '<br />');
                    $(formElement + " input:text").first().focus();
                    $(submitButton).attr('disabled', false);
                }
            }).always(function () {
                $(overlay).hide();
                $(submitButton).attr('disabled', false);
                $(submitButton).button("reset");
            });
        }
    });
    $(overlay).hide();
}

async function PrepareChartCanvas(chartId) {
    $("#" + chartId).remove(); //canvas
    let div = document.querySelector("." + chartId + "Container"); //canvas parent element
    div.insertAdjacentHTML("afterbegin", "<canvas id='" + chartId + "'></canvas>"); //adding the canvas again
}

async function UpdateGraph(graphType) {
    let zoneId = $(".zoneDropdownList").val();
    let zoneName = $(".zoneDropdownList").find(':selected').attr('data-value');
    let selectedDate = $("#toDate").val();

    if (graphType === "hourly") {
        $(".chartTimeOverlay").show();
        let operation = $("#operationsDropDownList").val();
        let operationsByHourAjaxRequestData = await GetOperationsByHour(selectedDate, zoneId, operation);
        let operationsHourLineChart = await GenerateChartTime(operationsByHourAjaxRequestData, selectedDate, zoneName);
        $(".chartTimeOverlay").hide();
    }

    if (graphType === "daily") {
        $(".chartDateOverlay").show();
        let requestUrl = `operations/json/count/byDate?zoneId=${zoneId}&date=${selectedDate}`;
        let operationsByDateAjaxRequestData = await GetAjaxData(requestUrl);
        let operationsDayBarChart = await GenerateChartDate(operationsByDateAjaxRequestData, selectedDate, zoneName, zoneId, "bar");
        $(".chartDateOverlay").hide();
    }
}

async function GenerateChartDate(jsonData, selectedDate, zoneName, zoneId, chartType) {

    const operationName = jsonData.map(o => o.operationName);
    const operationQuantity = jsonData.map(o => o.operationQuantity);
    const ctx = document.getElementById('chartDate');

    let config = await GetChartConfig(chartType, operationName, operationQuantity, selectedDate, zoneId, zoneName);

    return new Chart(ctx, config);
}

async function GetChartConfig(chartType, operationName, operationQuantity, selectedDate, zoneId, zoneName) {

    var lineTension = chartType === "line" ? 0.1 : 0;

    let config = {
        type: chartType,
        data: {
            labels: operationName,
            datasets: [
                {
                    backgroundColor: "#f54748",
                    label: zoneName,
                    data: operationQuantity,
                    lineTension: lineTension,
                    fill: chartType !== "line"
                }
            ]
        },
        options: {
            maintainAspectRatio: false,
            aspectRatio: 2,
            onResize: function (chart, size) {
            },
            legend: {
                display: false,
                onclick: function () {
                }
            },
            animation: {
                duration: 1100,
                onComplete: function () {

                }
            },
            scales: {
                yAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: 'Quantity'
                    },
                    ticks: {
                        beginAtZero: true
                    }
                }],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: 'Operation Name'
                    }
                }]
            },
            layout: {
                padding: {
                    top: 40,
                    bottom: 50
                }
            },
            plugins: {
                datalabels: {
                    color: '#000',
                    align: 'end',
                    anchor: 'end',
                    formatter: function (value, context) {
                        return value;
                    },
                    font: {
                        size: 16,
                        style: 'bold',
                    }
                }
            },
            onClick: async function (evt) {
                let activePoints = this.getElementsAtEvent(evt);

                if (activePoints[0]) {
                    let chartData = activePoints[0]['_chart'].config.data;
                    let idx = activePoints[0]['_index'];
                    let workcenterLabel = chartData.labels[idx];
                    let targetModal = "#OperationsListModal";
                    let refreshUrl = `Operations/list/date?date=${selectedDate}&zoneId=${zoneId}&workcenter=${workcenterLabel}`;
                    let title = zoneName + " " + workcenterLabel + " processed jobs for " + selectedDate

                    await TriggerModal("#mainBodyContent", targetModal, refreshUrl, "800px", "#343A40", "", title, true, 9999999, true);

                    $(targetModal).iziModal('open');

                    this.update();
                }
            }
        }
    };
    return config;
}

async function GenerateChartTime(jsonData, selectedDate, zoneName) {

    const OperationName = jsonData.map(o => o.hour);
    const OperationQuantity = jsonData.map(o => o.operationQuantity);
    const ctx = document.getElementById('chartTime');

    var config = {
        type: 'line',
        data: {
            labels: OperationName,
            datasets: [
                {
                    fill: false,
                    lineTension: 0.1,
                    borderColor: "#31326f",
                    data: OperationQuantity,
                    label: zoneName
                }
            ]
        },
        options: {
            maintainAspectRatio: false,
            aspectRatio: 2,
            onResize: function (chart, size) {
            },
            legend: {
                display: false,
                position: "top",
                labels: {
                    fontStyle: "800"
                },
                onclick: function () {
                }
            },
            animation: {
                duration: 1100,
                onComplete: function () {
                }
            },
            scales: {
                yAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: 'Quantity'
                    },
                    ticks: {
                        beginAtZero: true
                    }
                }],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: 'Hours'
                    }
                }]
            },
            layout: {
                padding: {
                    top: 40,
                    bottom: 50
                }
            },
            plugins: {
                datalabels: {
                    color: '#000',
                    align: 'end',
                    anchor: 'end',
                    formatter: function (value, context) {
                        return value;
                    },
                    font: {
                        size: 16,
                        style: 'bold',
                    }
                }
            },
            onClick: async function (evt) {
                let activePoints = this.getElementsAtEvent(evt);

                if (activePoints[0]) {
                    //console.log(activePoints);
                    let chartData = activePoints[0]['_chart'].config.data;
                    let idx = activePoints[0]['_index'];
                    let selectedZone = $("#zone").val();
                    let operation = $("#operationsDropDownList").val();
                    let label = chartData.labels[idx];
                    let refreshUrl = `Operations/list/hour?date=${selectedDate}&zoneId=${selectedZone}&hour=${label}&operation=${operation}`;
                    let targetModal = "#OperationsListModal";
                    let title = zoneName + " " + operation + " processed jobs for " + selectedDate + " (Hour : " + label + ")"

                    await TriggerModal("#mainBodyContent", targetModal, refreshUrl, "800px", "#343A40", "", title, true, 9999999, true);

                    $(targetModal).iziModal('open');

                    this.update();
                }
            }
        }
    };

    let myChart = new Chart(ctx, config);

    return myChart;
}

//set date elemetnt to todays date
//get date according to yyy-mm-dd format
function getDate() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
    return today;
}

//set opertaions to this values
function GetRefurbishingOperations() {
    $(document).ready(function () {
        var json = [{"OperationName": "DEVALV"}, {"OperationName": "FINAL "}, {"OperationName": "HYDRO "}, {"OperationName": "VALV  "}, {"OperationName": "VISUAL"}];
        $.each(json, function (index, value) {
            $('<option>', {
                value: value.OperationName,
                text: value.OperationName
            }).appendTo("#operationsDropDownList");
        });
    })
}

//**********************************
//Helpers

function GetAjaxContent(requestUrl, container, overlay, updateHistory, historyUrl, error) {
    $.get(requestUrl, function (data, textStatus, jqXHR) {
        $(overlay).show();
        $(container).html(data);
        InstantiateDataTable()
        $(overlay).hide();

        if (updateHistory === true) {
            var pageUrl = IsNullOrEmptyOrWhiteSpace(historyUrl) ? requestUrl : historyUrl;
            console.log(pageUrl);
            UpdateBrowserHistory(pageUrl);
        }
        //$(".overlay").hide()

    }).fail(function () {
        $(container).html(error);
        $(overlay).hide();
    });
}

function PostAjaxContent(requestUrl, container, overlay, updateHistory, historyUrl, error) {
    $.post(requestUrl, function (data, textStatus, jqXHR) {
        $(overlay).show();
        $(container).html(data);
        InstantiateDataTable()
        $(overlay).hide();

        if (updateHistory === true) {
            var pageUrl = IsNullOrEmptyOrWhiteSpace(historyUrl) ? requestUrl : historyUrl;
            console.log(pageUrl);
            UpdateBrowserHistory(pageUrl);
        }
    }).fail(function () {
        $(container).html(error);
        $(overlay).hide();
    });
}

async function RequestModalContent(modal, requestUrl) {
    return $.ajax({
        url: requestUrl,
        method: 'GET',
        data: {},
        cache: false,
        success: async function (data) {
            setTimeout(async function () {
                //var pageUrl = requestUrl.split('#')[0];
                //console.log(pageUrl);
                //UpdateBrowserHistory(pageUrl);
                $(modal + ' .iziModalContent').html(data).promise().done(async function () {
                    await InstantiateDataTableInModal(modal);
                    $(modal).iziModal('stopLoading');
                });
            }, 200);

        },
        error: function (xhr, textStatus, errorThrown) {
            Swal.fire({
                type: 'error',
                icon: 'error',
                title: 'Oops...',
                text: xhr.responseJSON.message
            });
            $(".swal2-container").css('z-index', 2147483510);
        }
    });
}

function IsNullOrEmptyOrWhiteSpace(value) {
    return value === null || value === "" || value === undefined;
}