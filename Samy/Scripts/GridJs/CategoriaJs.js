$(function () {
    debugger;
    $("#grid").jqGrid({
        url: "/Categorias/ListarCategorias",
        datatype: "json",
        mtype: "Get",
        colNames: ["Id", "Descripción","Acción"],
        colModel: [
            { key: true, hidden: true, name: "Id", index: "Id" },
            { key: false, name: "Descripcion", index: "Descripcion", editable: false },
            { name: 'act', index: 'act', width: 75, sortable: false }
        ],
        pager: $("#pager"),
        rowNum: 10,
        rowList: [10, 15],
        height: "100%",
        viewrecords: true,
        caption: "Categorías",
        emptyrecords: "No hay categorías",
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: "0"
        },
        gridComplete: function () {
            var ids = jQuery("#grid").getDataIDs();
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                be = "<input class='btn btn-warning' type='button' value='Editar' onclick=jQuery('#grid').editRow(" + cl + "); ></ids>";
                se = "<input class='btn btn-info' type='button' value='Detalles' onclick=jQuery('#grid').saveRow(" + cl + "); />";
                ce = "<input class='btn btn-danger' type='button' value='Eliminar' onclick=jQuery('#grid').restoreRow(" + cl + "); />";
                jQuery("#grid").setRowData(ids[i], { act: be + se + ce })
            }
        },
        autowidth: true,
        multiselect: false
    }).navGrid("#pager", { edit: false, add: false, del: false });
});

