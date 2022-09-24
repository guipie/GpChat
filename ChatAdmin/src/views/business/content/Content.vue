<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/business/content/Content.js此处编写
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
import extend from "@/extension/business/content/Content.js";
import ViewGrid from "@/components/basic/ViewGrid.vue";
var vueParam = {
  components: {
    ViewGrid
  },
  data () {
    return {
      table: {
        key: 'ContentGuid',
        footer: "Foots",
        cnName: '内容',
        name: 'content/Content',
        url: "/Content/",
        sortName: "CreateDate"
      },
      extend: extend,
      editFormFields: {},
      editFormOptions: [],
      searchFormFields: {},
      searchFormOptions: [],
      columns: [
        { field: 'ParentGuid', title: '转发ID', type: 'guid', sortable: true, hidden: true },
        { field: 'Contents', title: '内容', type: 'string', link: true, width: 220, require: true, align: 'left' },
        { field: 'AuditStatus', title: '审核状态', type: 'int', width: 90, require: true, align: 'left' },
        { field: 'PicCount', title: '图片个数', type: 'int', width: 80, require: true, align: 'left' },
        { field: 'CollectCount', title: '收藏数', type: 'int', width: 80, require: true, align: 'left' },
        { field: 'ShareCount', title: '分享数', type: 'int', width: 80, require: true, align: 'left' },
        { field: 'PraiseCount', title: '点赞数', type: 'int', width: 80, require: true, align: 'left' },
        { field: 'XinPraiseCount', title: '新赞数', type: 'int', width: 80, require: true, align: 'left' },
        { field: 'CommentCount', title: '评论数', type: 'int', width: 80, require: true, align: 'left' },
        { field: 'CreateDate', title: '创建日期', type: 'datetime', width: 150, require: true, align: 'left', sortable: true },
        { field: 'Creator', title: '创建人', type: 'string', width: 130, require: true, align: 'left' }],
      detail: {
        cnName: "内容文件",
        columns: [{ field: 'Guid', title: 'Guid', type: 'guid', width: 90, hidden: true, readonly: true, require: true, align: 'left' },
        { field: 'ContentGuid', title: 'ContentGuid', type: 'guid', width: 90, require: true, align: 'left', sortable: true },
        { field: 'Name', title: 'Name', type: 'string', width: 120, require: true, align: 'left' },
        { field: 'UploadPath', title: 'UploadPath', type: 'string', width: 220, require: true, align: 'left' },
        { field: 'ContentType', title: 'ContentType', type: 'string', width: 90, require: true, align: 'left' },
        { field: 'Length', title: 'Length', type: 'float', width: 90, require: true, align: 'left' },
        { field: 'IsTemp', title: 'IsTemp', type: 'bool', width: 90, require: true, align: 'left' },
        { field: 'CreateDate', title: 'CreateDate', type: 'datetime', width: 90, require: true, align: 'left', sortable: true },
        { field: 'CreateID', title: 'CreateID', type: 'int', width: 80, hidden: true, require: true, align: 'left' },
        { field: 'Creator', title: 'Creator', type: 'string', width: 130, require: true, align: 'left' }],
        sortName: "CreateDate",
        key: "Guid"
      }
    };
  }
};
export default vueParam;
</script>
