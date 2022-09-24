<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/business/settings/Sys_Agreement.js此处编写
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
import extend from "@/extension/business/settings/Sys_Agreement.js";
import ViewGrid from "@/components/basic/ViewGrid.vue";
var vueParam = {
  components: {
    ViewGrid
  },
  data () {
    return {
      table: {
        key: 'Id',
        footer: "Foots",
        cnName: 'App协议/文档配置',
        name: 'settings/Sys_Agreement',
        url: "/Sys_Agreement/",
        sortName: "Id"
      },
      extend: extend,
      editFormFields: { "DicNo": "", "Title": "", "Html": "" },
      editFormOptions: [[{ "dataKey": "agreement", "data": [], "title": "app协议", "required": true, "field": "DicNo", "type": "select" }],
      [{ "title": "标题", "required": true, "field": "Title", "type": "text" }],
      [{ "title": "内容", "required": true, "field": "Html", "type": "editor" }]],
      searchFormFields: { "Title": "" },
      searchFormOptions: [
        [{ "title": "标题", "field": "Title", "type": "text" }],
        [{ "dataKey": "agreement", "data": [], "title": "app协议", "field": "DicNo", "type": "checkbox" }]
      ],
      columns: [
        { field: 'Id', title: '编号', type: 'int', width: 90, hidden: true, readonly: true, require: true, align: 'left' },
        { field: 'DicNo', title: 'app协议', type: 'string', bind: { key: 'agreement', data: [] }, width: 90, require: true, align: 'left', sortable: true },
        { field: 'Title', title: '标题', link: true, type: 'string', width: 90, require: true, align: 'left' },
        { field: 'Html', title: '内容', type: 'string', width: 90, require: true },
        { field: 'CreateDate', title: '创建时间', type: 'datetime', sort: true, width: 90, require: true, align: 'left', sortable: true },
        { field: 'Creator', title: '创建人', type: 'string', width: 130, require: true, align: 'left' },
        { field: 'ModifyDate', title: '更新时间', type: 'datetime', sort: true, width: 90, align: 'left', sortable: true }
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
