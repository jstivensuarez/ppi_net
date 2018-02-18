$(function () {
    debugger;
    $("#grid").jqGrid({
        url: "/TipoDocumentoes/ListarTiposDocumentos",
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
        caption: "Tipos de documentos",
        emptyrecords: "No hay Tipos de documentos",
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