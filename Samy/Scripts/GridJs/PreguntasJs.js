$(function () {
    debugger;
    $("#grid").jqGrid({
        url: "/Preguntas/ListarPreguntas",
        datatype: "json",
        mtype: "Get",
        colNames: ["Id", "Descripción"],
        colModel: [
            { key: true, hidden: true, name: "Id", index: "Id" },
            { key: false, name: "Descripcion", index: "Descripcion", editable: false }
        ],
        pager: $("#pager"),
        rowNum: 10,
        rowList: [10,15],
        height: "100%",
        viewrecords: true,
        caption: "Preguntas",
        emptyrecords: "No hay preguntas",
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: "0"
        },
        autowidth: true,
        multiselect: false
    });
});