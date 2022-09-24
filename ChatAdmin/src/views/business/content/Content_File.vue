<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/business/content/Content_File.js此处编写
 -->
<template>
    <div>
        <view-grid
                   ref="grid"
                   :columns="columns"
                   :detail="detail"
                   :editFormFields="editFormFields"
                   :editFormFileds="editFormFields"
                   :editFormOptions="editFormOptions"
                   :searchFormFields="searchFormFields"
                   :searchFormFileds="searchFormFields"
                   :searchFormOptions="searchFormOptions"
                   :table="table"
                   :extend="extend">
        </view-grid>
    </div>
</template>

<script>
    import extend from "@/extension/business/content/Content_File.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'Guid',
                    footer: "Foots",
                    cnName: '内容文件',
                    name: 'content/Content_File',
                    url: "/Content_File/",
                    sortName: "CreateDate"
                },
                extend: extend,
                editFormFields: {},
                editFormOptions: [],
                searchFormFields: {},
                searchFormOptions: [],
                columns: [{field:'Guid',title:'Guid',type:'guid',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'ContentGuid',title:'ContentGuid',type:'guid',width:90,require:true,align:'left',sortable:true},
                       {field:'Name',title:'Name',type:'string',width:120,require:true,align:'left'},
                       {field:'UploadPath',title:'UploadPath',type:'string',width:220,require:true,align:'left'},
                       {field:'ContentType',title:'ContentType',type:'string',width:90,require:true,align:'left'},
                       {field:'Length',title:'Length',type:'float',width:90,require:true,align:'left'},
                       {field:'IsTemp',title:'IsTemp',type:'bool',width:90,require:true,align:'left'},
                       {field:'CreateDate',title:'CreateDate',type:'datetime',width:90,require:true,align:'left',sortable:true},
                       {field:'CreateID',title:'CreateID',type:'int',width:80,hidden:true,require:true,align:'left'},
                       {field:'Creator',title:'Creator',type:'string',width:130,require:true,align:'left'}],
                detail: {
                    cnName:"#detailCnName",
                    columns: [],
                    sortName: "",
                    key:""
                }
            };
        }
    };
    export default vueParam;
</script>
