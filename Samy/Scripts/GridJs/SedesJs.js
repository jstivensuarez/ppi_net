$(function () {
    debugger;
    $("#grid").jqGrid({
        url: "/Sedes/ListarSedes",
        datatype: "json",
        mtype: "Get",
        colNames: ["Id", "Nombre"],
        colModel: [
            { key: true, hidden: true, name: "Id", index: "Id" },
            { key: false, name: "Nombre", index: "Nombre", editable: false }
        ],
        pager: $("#pager"),
        rowNum: 10,
        rowList: [10,15],
        height: "100%",
        viewrecords: true,
        caption: "Sedes",
        emptyrecords: "No hay sedes",
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