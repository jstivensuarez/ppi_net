$(function () {
    debugger;
    $("#grid").jqGrid({
        url: "/Alumnoes/ListarAlumnos",
        datatype: "json",
        mtype: "Get",
        colNames: ["Id", "Nombre", "Documento", "Primer Apellido", "Segundo Apellido", "Correo"],
        colModel: [
            { key: true, hidden: true, name: "Id", index: "Id" },
            { key: false, name: "Nombre", index: "Nombre", editable: false },
            { key: false, name: "Documento", index: "Documento", editable: false },
            { key: false, name: "PrimerApellido", index: "PrimerApellido", editable: false },
            { key: false, name: "SegundoApellido", index: "SegundoApellido", editable: false },
            { key: false, name: "Correo", index: "Correo", editable: false },
        ],
        pager: $("#pager"),
        rowNum: 10,
        rowList: [10, 15],
        height: "100%",
        viewrecords: true,
        caption: "Alumnos",
        emptyrecords: "No hay Alumnos",
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