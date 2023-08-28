
//function FetchEventAndRenderCalendar() {
//    events = [];

//    $.ajax({
//        type: "GET",
//        url: `Logistics/Trips/GetLogisticsTrip`,
//        success: function (data) {
//            $.each(data, function (i, v) {
//                events.push({
//                    id: v.id,
//                    title: v.name,
//                    description: v.description,
//                    start: v.expectedStartDate,
//                    end: v.expectedArrivalDate,
//                    color: v.color.hex,
//                    modelUrl: `Logistics/Trips/ShowTrip?id=${v.id}`
//                });
//            })
//            draw(events);
//        },
//        error: function (error) {
//            alert('Failed to load Trips');
//        }
//    })

//    $.ajax({
//        type: "GET",
//        url: `Logistics/Routes/GetLogisticsRoute`,
//        success: function (data) {
//            $.each(data, function (i, v) {
//                events.push({
//                    id: v.id,
//                    title: v.name,
//                    description: v.description,
//                    start: v.expectedStartDate,
//                    end: v.expectedArrivalDate,
//                    color: v.color.hex,
//                    display: 'list-item',
//                    modelUrl: `Logistics/Routes/ShowRoute?id=${v.id}`
//                });
//            })
//            draw(events);
//        },
//        error: function (error) {
//            alert('Failed to load routes');
//        }
//    })
//}

//async function draw(data) {
//    var calendarEl = document.getElementById('calendar');

//    var calendar = new FullCalendar.Calendar(calendarEl, {
//        initialView: 'dayGridMonth',
//        initialDate: new Date(),
//        headerToolbar: {
//            left: 'prev,next today',
//            center: 'title',
//            right: 'dayGridMonth,timeGridWeek,timeGridDay'
//        },
//        events: data,
//        eventClick: async function (calEvent) {
//            if (calEvent.event.extendedProps.modelUrl.includes("Trip")) {
//                await TriggerModal('#mainBodyContent', "#TripIzModal", calEvent.event.extendedProps.modelUrl, "800px", "#343A40", "",
//                    `<lord-icon src="https://cdn.lordicon.com/gqzfzudq.json"
//                           trigger="loop"
//                           colors="primary:#000000,secondary:#08a88a">
//                           </lord-icon> Show Trip`, true, 99999, true);
//                $("#TripIzModal").iziModal('open');
//            }
//            else {
//                await TriggerModal('#mainBodyContent', "#RouteIzModal", calEvent.event.extendedProps.modelUrl, "800px", "#343A40", "",
//                    `<lord-icon src="https://cdn.lordicon.com/nxaaasqe.json"
//                           trigger="loop"
//                           colors="primary:#000000,secondary:#08a88a">
//                           </lord-icon> Show Route`, true, 99999, true);
//                $("#RouteIzModal").iziModal('open');
//            }
//        },

//        editable: false, // Don't allow editing of events
//        //handleWindowResize: true,
//        //weekends: false, // Hide weekends
//        //defaultView: 'agendaWeek', // Only show week view
//        //header: false, // Hide buttons/titles
//        //minTime: '07:30:00', // Start time for the calendar
//        //maxTime: '22:00:00', // End time for the calendar
//        //columnFormat: {
//        //    week: 'ddd' // Only show day of the week names
//        //},

//        displayEventTime: true, // Display event time
//        selectable: true,
//        select: function (start, end) {
//            selectedEvent = {
//                //eventID: 0,
//                title: '',
//                description: '',
//                start: start,
//                end: end,
//                allDay: false,
//                color: '#777777',
//                backgroundColor: '#eeeef0' 
//            };
//            openAddForm();
//            //$('#calendar').fullCalendar('unselect');
//        },
//        eventDrop: function (event) {
//            //var data = {
//            //    id: event.oldEvent.id,
//            //    name: event.oldEvent.title,
//            //    actualStartDate: event.event.start,
//            //    actualArrivalDate: event.event.end
//            //};
//            //EditEvent(data);
//        },
//        eventColor: '#dc3543',
//        eventBackgroundColor: '#dc3543',
//    });
//    calendar.render();
//}

//async function openAddForm() {
//    if (selectedEvent.title != "") {
//        $('#hdEventID').val(selectedEvent.eventID);
//        $('#txtSubject').val(selectedEvent.title);
//        $('#txtStart').val(selectedEvent.start/*.format('DD/MM/YYYY HH:mm A')*/);
//        $('#chkIsFullDay').prop("checked", selectedEvent.allDay || false);
//        $('#chkIsFullDay').change();
//        $('#txtEnd').val(selectedEvent.end != null ? selectedEvent.end : '');
//        $('#txtDescription').val(selectedEvent.description);
//        $('#ddThemeColor').val(selectedEvent.color);
//    }

//    if (selectedEvent.title == "") {
//        $('#hdEventID').val(selectedEvent.eventID);
//        $('#txtSubject').val(selectedEvent.title);
//        $('#txtStart').val(selectedEvent.start.start.toISOString()/*.format('DD/MM/YYYY HH:mm A')*/);
//        $('#chkIsFullDay').prop("checked", selectedEvent.allDay || false);
//        $('#chkIsFullDay').change();
//        $('#txtEnd').val(selectedEvent.start.end.toISOString() /*!= null ? selectedEvent.end.format('DD/MM/YYYY HH:mm A') : ''*/);
//        $('#txtDescription').val(selectedEvent.description);
//        $('#ddThemeColor').val(selectedEvent.color);

//        await TriggerModal('#mainBodyContent', "#AddTripModal", "Logistics/Trips/AddTrip", "450px", "#343A40", "", "Add Trip", true, 2147483647, true);

//        $(document).on('opened', '#AddTripModal', function (e) {
//            $('#ExpectedStartDate').val(selectedEvent.start.start.toISOString().substring(0, 16));
//            $('#ExpectedArrivalDate').val(selectedEvent.start.end.toISOString().substring(0, 16));
//        });

//        $("#AddTripModal").iziModal('open');
//    }
//}

//async function openEditForm() {
//    if (selectedEvent.title != "") {
//        $('#myModal').modal('hide');
//        $('#myModalSaveRoute').modal();
//    }
//}

//function InstantiateMagicSuggest(field, placeHolder, method, dataSource, maxSelection, selectionPosition, valueField, submitButton, displayField, queryParamName, disableSubmitIfInvalid) {

//    var query = queryParamName !== null ? queryParamName : 'query';
//    var ms = $(field).magicSuggest({
//        allowFreeEntries: false,
//        placeholder: placeHolder,
//        selectionStacked: true,
//        selectionPosition: selectionPosition,
//        useTabKey: true,
//        expandOnFocus: false,
//        highlight: false,
//        maxDropHeight: 160,
//        maxSelection: maxSelection,
//        minChars: 1,
//        displayField: displayField,
//        hideTrigger: true,
//        allowDuplicates: false,
//        useCommaKey: true,
//        method: method,
//        data: dataSource,
//        noSuggestionText: 'No result matching the term {{query}}',
//        required: true,
//        selectFirst: true,
//        typeDelay: 15,
//        valueField: valueField,
//        queryParam: query
//    });
//    if (disableSubmitIfInvalid) {
//        $(ms).on('selectionchange', function (e, m) {
//            var valuesCount = this.getValue();

//            if (valuesCount.length > 0) {
//                $(submitButton).prop("disabled", false);
//            }
//            else {
//                $(submitButton).prop("disabled", true);
//            }
//        });
//    }

//    return ms;
//}

////function PrepareAndPostFormAjax(formElement, errorContainer, submitButton, postUrl, modalToClose, multipleModals, partialRefresh, partialRefreshContainer, partialRefreshSource, anotherOneCheckBox, afterActionCallBack) {

////    $(formElement).on('submit', function (e) {
////        e.preventDefault();

////        var form = $(this);

////        $(errorContainer).html("");

////        if (form.valid()) {
////            $(submitButton).button("loading");

////            $.ajax({
////                type: "POST",
////                url: postUrl,
////                data: form.serialize(),
////                success: function (response) {

////                    //Handle multiple modal refresh if set to true
////                    //
////                    if (multipleModals) {
////                        $(".ModalRefreshButton").click();
////                    }

////                    //Handles partial refresh if set to true
////                    //The source and container must be provided to avoid errors
////                    //
////                    if (partialRefresh) {
////                        if (!IsNullOrEmptyOrWhiteSpace(partialRefreshContainer) && !IsNullOrEmptyOrWhiteSpace(partialRefreshSource)) {
////                            $.get(partialRefreshSource, function (data) {
////                                $(partialRefreshContainer).hide().html(data).fadeIn();
////                            });
////                        }
////                        else {
////                            alert("Lack of attention error (LOL): Missing partial refresh data");
////                        }
////                    }

////                    if (IsNullOrEmptyOrWhiteSpace(anotherOneCheckBox)) {
////                        $(modalToClose).iziModal('close');
////                    }
////                    else {
////                        if (!$(anotherOneCheckBox).is(':checked')) {
////                            $(modalToClose).iziModal('close');
////                        }
////                        else {
////                            $(".ModalRefreshButton").click();
////                        }
////                    }
////                    if (!IsNullOrEmptyOrWhiteSpace(afterActionCallBack)) {
////                        afterActionCallBack();
////                    }
////                },
////                failure: function (response) {
////                    alert("failed" + response);
////                },
////                error: function (xhr, textStatus, errorThrown) {

////                    $(errorContainer).append(xhr.responseText + '<br />');
////                    //Command: toastr["error"](xhr.responseText + '<br />');
////                    //}

////                    $(formElement + " input:text").first().focus();
////                }
////            }).always(function () {
////                $(submitButton).button("reset");
////            });
////        }
////    });
////}

//function PrepareAndPostFormAjaxWithoutSubmit(formElement, errorContainer, postUrl, modalToClose, multipleModals, partialRefresh, partialRefreshContainer, partialRefreshSource, anotherOneCheckBox, afterActionCallBack) {
//    var form = $(this);
//    //$(errorContainer).replaceAll('');

//    $.ajax({
//        type: "GET",
//        url: postUrl,
//        data: form.serialize(),
//        success: function (response) {
//            $(errorContainer).text(" ");

//            //Handle multiple modal refresh if set to true
//            //
//            if (multipleModals) {
//                $(".ModalRefreshButton").click();
//            }

//            //Handles partial refresh if set to true
//            //The source and container must be provided to avoid errors
//            //
//            if (partialRefresh) {
//                if (!IsNullOrEmptyOrWhiteSpace(partialRefreshContainer) && !IsNullOrEmptyOrWhiteSpace(partialRefreshSource)) {
//                    $.get(partialRefreshSource, function (data) {
//                        $(partialRefreshContainer).hide().html(data).fadeIn();
//                    });
//                }
//                else {
//                    alert("Lack of attention error (LOL): Missing partial refresh data");
//                }
//            }

//            if (IsNullOrEmptyOrWhiteSpace(anotherOneCheckBox)) {
//                $(modalToClose).iziModal('close');
//            }
//            else {
//                if (!$(anotherOneCheckBox).is(':checked')) {
//                    $(modalToClose).iziModal('close');
//                }
//                else {
//                    $(".ModalRefreshButton").click();
//                }
//            }
//            if (!IsNullOrEmptyOrWhiteSpace(afterActionCallBack)) {
//                afterActionCallBack();
//            }
//        },
//        failure: function (response) {
//            alert("failed" + response);
//        },
//        error: function (xhr, textStatus, errorThrown) {
//            $(errorContainer).text(xhr.responseText);
//            $(formElement + " input:text").first().focus();
//        }
//    });
//}

       
