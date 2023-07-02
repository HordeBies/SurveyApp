var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/Statistics/GetAll" },
        columns: [
            { data: 'id', "width": "10%" },
            { data: 'name', "width": "25%" },
            { data: null, "render": function (data) { return `<div class="tooltipdiv"><a class="" href="#" onmouseout="document.getElementById('tooltip_${data.id}').innerHTML = 'Copy to Clipboard';"  onclick="navigator.clipboard.writeText('${data.participantUrl}');document.getElementById('tooltip_${data.id}').innerHTML = 'Copied!';"><span class="tooltiptext" id="tooltip_${data.id}">Copy to Clipboard</span>${data.participantUrl}</button></div>` }, "width": "25%" },
            { data: 'statisticsUrl', "render": function (data) {return `<a href="${data}">Show Stats</a>` } , "width": "25%" },

        ]
    });
}