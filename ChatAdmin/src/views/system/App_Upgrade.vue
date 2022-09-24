<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/system/system/App_Upgrade.js此处编写
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
  import extend from "@/extension/system/system/App_Upgrade.js";
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
          cnName: '版本升级',
          name: 'system/App_Upgrade',
          url: "/App_Upgrade/",
          sortName: "Id"
        },
        extend: extend,
        editFormFields: {
          Version: "",
          Url: "guxin.link/apk/guxin_test.apk",
          Desc: "",
          Force: 1
        },
        editFormOptions: [
          [{
            "title": "版本号",
            "required": true,
            "field": "Version"
          }],
          [{
            "title": "下载地址",
            "required": true,
            "field": "Url"
          }],
          [{
            "title": "升级描述",
            "required": true,
            "field": "Desc",
            "type": "textarea"
          }],
          [{
            "dataKey": "enable",
            "required": true,
            "title": "强制升级",
            "field": "Force",
            "type": "switch"
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
            field: 'Version',
            title: '版本号',
            type: 'string',
            width: 90,
            require: true,
            align: 'left',
            sortable: true
          },
          {
            field: 'Url',
            title: '下载地址',
            type: 'string',
            width: 220,
            require: true,
            align: 'left'
          },
          {
            field: 'Desc',
            title: '升级描述',
            type: 'string',
            width: 220,
            require: true,
            align: 'left'
          },
          {
            field: 'Force',
            title: '是否强制升级',
            type: 'bool',
            bind: {
              key: 'enable',
              data: []
            },
            width: 90,
            require: true,
            align: 'left'
          },
          {
            field: 'CreateDate',
            title: 'CreateDate',
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
            title: 'Creator',
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
