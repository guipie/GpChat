<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/system/system/Complaint.js此处编写
 -->
<template>
  <div>
    <view-grid ref="grid" :columns="columns" :detail="detail" :editFormFields="editFormFields"
      :editFormFileds="editFormFields" :editFormOptions="editFormOptions" :searchFormFileds="searchFormFileds"
      :searchFormOptions="searchFormOptions" :table="table" :extend="extend">
    </view-grid>
  </div>
</template>

<script>
  import extend from "@/extension/system/Complaint.js";
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
          cnName: '投诉内容',
          name: 'Complaint',
          url: "/Complaint/",
          sortName: "Id"
        },
        extend: extend,
        editFormFields: {},
        editFormOptions: [],
        searchFormFileds: {
          "ComplaintDic": "",
          "ComplaintType": []
        },
        searchFormOptions: [
          [{
            "title": "投诉类型",
            "field": "ComplaintDic",
            "type": "input"
          }, {
            "dataKey": "ComplaintType",
            "title": "类别",
            "field": "ComplaintType",
            "type": "checkbox"
          }]
        ],
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
            field: 'ComplaintDic',
            title: '投诉类型',
            type: 'string',
            width: 90,
            require: true,
            align: 'left',
            sortable: true
          },
          {
            field: 'ComplaintType',
            title: '投诉内容类别',
            type: 'int',
            bind: {
              key: 'ComplaintType',
              data: []
            },
            width: 90,
            require: true,
            align: 'left'
          },
          {
            field: 'ComplaintGuid',
            title: 'ComplaintGuid',
            type: 'guid',
            width: 90,
            hidden: true,
            require: true,
            align: 'left'
          },
          {
            field: 'Remark',
            title: '备注',
            type: 'string',
            width: 220,
            align: 'left'
          },
          {
            field: 'Pics',
            title: '图片',
            type: 'img',
            width: 220,
            align: 'left'
          },
          {
            field: 'CreateDate',
            title: '投诉时间',
            type: 'datetime',
            width: 90,
            require: true,
            align: 'left',
            sortable: true
          },
          {
            field: 'CreateID',
            title: '投诉用户',
            type: 'int',
            width: 80,
            hidden: true,
            require: true,
            align: 'left'
          },
          {
            field: 'Creator',
            title: '投诉用户',
            type: 'string',
            width: 130,
            require: true,
            align: 'left'
          },
          {
            field: 'AuditDate',
            title: 'AuditDate',
            type: 'datetime',
            width: 150,
            hidden: true,
            align: 'left',
            sortable: true
          },
          {
            field: 'Auditor',
            title: 'Auditor',
            type: 'string',
            width: 90,
            hidden: true,
            align: 'left'
          },
          {
            field: 'AuditStatus',
            title: '内容审核状态',
            type: 'int',
            width: 90,
            hidden: true,
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
