////////var endpoint = "https://localhost:44307/api";

////////async function getajaxdata(url) {
////////    const result = await fetch(url);
////////    return await result.json();
////////}

////////async function getdata() {
////////    $(document).ready(async function () {
////////        var json = await getajaxdata(`${endpoint}/users`);
////////        $.each(json, function (index, value) {
////////            $('<option>', {
////////                value: value.id,
////////                text: value.username
////////            }).appendto("#users");
////////        });

////////        var json2 = await getajaxdata(`${endpoint}/drivers`);
////////        $.each(json2, function (index, value) {
////////            $('<option>', {
////////                value: value.userid,
////////                text: value.name
////////            }).appendt
////////var formdata = json.stringify($("#myform").serializearray());

////////$.ajax({
////////    type: "post",
////////    url: `${endpoint}/drivers`,
////////    data: `{${formdata}}`,
////////    success: function () { },
////////    datatype: "json",
////////    contenttype: "application/json"
////////});

////$(document).ready(function () {
////var events = [];
////var api = 'https://localhost:44307/';

////$.ajax({
////    type: "GET",
////    url: `https://localhost:44307/api/Route`,
////    success: function (data) {
////        $.each(data, function (i, v) {
////            events.push({
////                title: v.Name,
////                description: v.Description,
////                start: moment(v.Start),
////                end: v.End != null ? moment(v.End) : null,
////                color: v.Color,
////                allDay: v.IsFullDay
////            });
////        })
////        GenerateCalender(events);
////    },
////    error: function (error) {
////        alert('failed');
////    }
////})

////function GenerateCalender(events) {
////    $('#calendar').fullCalendar('destroy');
////    $('#calendar').fullCalendar({
////        contentHeight: 400,
////        defaultDate: new Date(),
////        timeFormat: 'h(:mm)a',
////        header: {
////            left: 'pre,next today',
////            center: 'title',
////            right: 'month, basicWeek,basicDay,agenda'
////        },
////        eventLimit: true,
////        eventColor: '#378006',
////        events:events
////    })

////}

////            /*form')[0];*/
//////form.addeventlistener('submit', handleformsubmit);


//////document.getElementById("postSubmit").onclick = function (e) {
//////    make_json(document.getElementById("myForm"));
//////}


//////function make_json(form) {
//////    var json = {};

//////    var elements = form.elements;

//////    for (var i = 0, element; element = elements[i++];) {
//////        if (element.type == "text" && element.value != "" && element.value != "date") {
//////            json[element.name] = element.value;
//////        }
//////    }

//////    var html = JSON.stringify(json, null, 4);
//////    document.getElementById('output').innerHTML = html;
//////    return false;
//////}



