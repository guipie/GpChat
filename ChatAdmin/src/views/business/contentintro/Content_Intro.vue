<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/business/contentintro/Content_Intro.js此处编写
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
    import extend from "@/extension/business/contentintro/Content_Intro.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'IntroGuid',
                    footer: "Foots",
                    cnName: '简介',
                    name: 'contentintro/Content_Intro',
                    url: "/Content_Intro/",
                    sortName: "CreateDate"
                },
                extend: extend,
                editFormFields: {},
                editFormOptions: [],
                searchFormFields: {},
                searchFormOptions: [],
                columns: [{field:'IntroGuid',title:'IntroGuid',type:'guid',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'BeginDate',title:'内容',type:'datetime',width:150,require:true,align:'left',sortable:true},
                       {field:'EndDate',title:'图片个数',type:'datetime',width:150,require:true,align:'left',sortable:true},
                       {field:'Location',title:'收藏数',type:'string',width:90,require:true,align:'left'},
                       {field:'Introduction',title:'分享转发数',type:'string',width:220,require:true,align:'left'},
                       {field:'PicCount',title:'新赞数',type:'int',width:80,require:true,align:'left'},
                       {field:'CreateDate',title:'CreateDate',type:'datetime',width:150,require:true,align:'left',sortable:true},
                       {field:'CreateID',title:'CreateID',type:'int',width:80,hidden:true,require:true,align:'left'},
                       {field:'Creator',title:'Creator',type:'string',width:130,require:true,align:'left'},
                       {field:'AuditDate',title:'AuditDate',type:'datetime',width:150,align:'left',sortable:true},
                       {field:'Auditor',title:'Auditor',type:'string',width:90,align:'left'},
                       {field:'AuditStatus',title:'内容审核状态',type:'int',width:90,require:true,align:'left'},
                       {field:'PraiseCount',title:'PraiseCount',type:'int',width:80,require:true,align:'left'}],
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
