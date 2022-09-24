<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/business/notice/User_Sys_Notice.js此处编写
 -->
<template>
  <div>
    <view-grid ref="grid" :columns="columns" :detail="detail" :editFormFields="editFormFields"
      :editFormFileds="editFormFields" :editFormOptions="editFormOptions" :searchFormFields="searchFormFields"
      :searchFormFileds="searchFormFields" :searchFormOptions="searchFormOptions" :table="table" :extend="extend">
    </view-grid>
  </div>
</template>

<script>
  import extend from "@/extension/business/notice/User_Sys_Notice.js";
  import ViewGrid from "@/components/basic/ViewGrid.vue";
  var vueParam = {
    components: {
      ViewGrid
    },
    data() {
      return {
        table: {
          key: 'Id',
          footer: "Foots",
          cnName: '系统公告',
          name: 'notice/User_Sys_Notice',
          url: "/User_Sys_Notice/",
          sortName: "Id"
        },
        extend: extend,
        editFormFields: {
          "Type": "",
          "Title": "",
          "Summary": "",
          "Contents": ""
        },
        editFormOptions: [
          [{
            "dataKey": "notice",
            "data": [],
            "title": "公告类型",
            "required": true,
            "field": "Type",
            "type": "select"
          }],
          [{
            "title": "标题",
            "required": true,
            "field": "Title",
            "type": "text"
          }],
          [{
            "title": "简介",
            "required": true,
            "field": "Summary",
            "type": "textarea"
          }],
          [{
            "title": "公告内容",
            "required": true,
            "field": "Contents",
            "type": "editor"
          }]
        ],
        searchFormFields: {},
        searchFormOptions: [],
        columns: [{
            field: 'Id',
            title: 'Id',
            type: 'int',
            width: 90,
            hidden: true,
            readonly: true,
            require: true,
            align: 'left'
          },
          {
            field: 'Type',
            title: '公告类型',
            bind: {
              key: 'notice',
              data: []
            },
            type: 'radio',
            width: 90,
            require: true,
            align: 'left',
            sortable: true
          },
          {
            field: 'Title',
            title: '标题',
            type: 'string',
            width: 90,
            align: 'left'
          },
          {
            field: 'Summary',
            title: '简介',
            type: 'string',
            width: 220,
            align: 'left'
          },
          {
            field: 'Contents',
            title: '内容',
            type: 'string',
            hidden: true,
            width: 220,
            align: 'left'
          },
          {
            field: 'CreateDate',
            title: '发布时间',
            type: 'datetime',
            width: 90,
            require: true,
            align: 'left',
            sortable: true
          },
          {
            field: 'CreateID',
            title: 'CreateID',
            type: 'int',
            width: 80,
            hidden: true,
            require: true,
            align: 'left'
          },
          {
            field: 'Creator',
            title: '发布人',
            type: 'string',
            width: 130,
            require: true,
            align: 'left'
          }
        ],
        detail: {
          cnName: "#detailCnName",
          columns: [],
          sortName: "",
          key: ""
        }
      };
    }
  };
  export default vueParam;
</script>
