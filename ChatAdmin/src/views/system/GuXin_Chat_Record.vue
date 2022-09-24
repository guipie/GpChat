<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/system/system/GuXin_Chat_Record.js此处编写
 -->
<template>
  <div>
    <view-grid ref="grid" :columns="columns" :detail="detail" :editFormFields="editFormFields"
      :editFormFileds="editFormFields" :editFormOptions="editFormOptions" :searchFormFields="searchFormFields"
      :searchFormFileds="searchFormFields" :searchFormOptions="searchFormOptions" :table="table" :extend="extend">
    </view-grid>
    <el-dialog title="提示" :visible.sync="table.dialogVisible" width="30%" @close="closeVoice">
      <el-form :inline="true" class="demo-form-inline">
        <el-form-item label="密钥:">
          <el-input v-model="table.miyao" placeholder="输入密钥显示明文"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="showContent">显示</el-button>
        </el-form-item>
        <el-form-item label="明文:">
          <span v-if="table.selectRow.Type=='ChatText'">{{table.selectRow.Content}}</span>
          <div v-else-if="table.selectRow.Type=='ChatVoice'" id="voice">
          </div>
          <div v-else-if="table.selectRow.Type=='ChatImage'" id="voice">
            <el-image style="width: 200px; height: 200px" :src="table.selectRow.Content" :z-index="1000000"
              :preview-src-list="[table.selectRow.Content]">
            </el-image>
          </div>
        </el-form-item>
      </el-form>
    </el-dialog>
  </div>
</template>

<script>
import extend from "@/extension/system/GuXin_Chat_Record.js";
import ViewGrid from "@/components/basic/ViewGrid.vue";
import { decrypt } from "@/utilities/cryptoJs";
import 'xgplayer';
import Music from 'xgplayer-music/dist';
let currentMusic;
var vueParam = {
  components: {
    ViewGrid
  },
  data () {
    return {
      table: {
        key: 'Id',
        footer: "Foots",
        cnName: '聊天记录',
        name: 'system/GuXin_Chat_Record',
        url: "/GuXin_Chat_Record/",
        sortName: "Id",
        miyao: "",
        dialogVisible: false,
        selectRow: {}
      },
      extend: extend,
      editFormFields: {},
      editFormOptions: [],
      searchFormFields: {},
      searchFormOptions: [],
      columns: [
        { field: 'Id', title: '记录编号', type: 'int', width: 90, readonly: true, require: true, align: 'left' },
        { field: 'Content', title: '发送内容', type: 'string', width: 220, align: 'left', sortable: true },
        { field: 'Type', title: '消息类别', type: 'string', width: 220, align: 'left' },
        { field: 'CreateID', title: '发送者', type: 'int', width: 80, require: true, align: 'left' },
        { field: 'ToUserId', title: '接受者', type: 'string', width: 90, require: true, align: 'left' },
        { field: 'CreateDate', title: '发送时间', type: 'datetime', width: 90, require: true, align: 'left', sortable: true }
      ],
      detail: {
        cnName: "#detailCnName",
        columns: [],
        sortName: "",
        key: ""
      }
    };
  },
  methods: {
    showContent () {
      console.log(this.table.selectRow);
      if (this.table.miyao != 'guxin.link')
        return this.$message({
          type: 'warning',
          message: '密钥输入错误.'
        });
      else
      {
        this.table.selectRow.Content = decrypt(this.table.selectRow.Content);
        if (this.table.selectRow.Type == 'ChatVoice')
          currentMusic = new Music({
            id: 'voice',
            url: [
              {
                src: this.table.selectRow.Content,
                name: '语音',
                vid: '000001',
                poster: require("@/assets/imgs/logo.png")
              }
            ],
            volume: 0.8,
            width: 400,
            height: 50
          })
      }
    },
    closeVoice () {
      if (this.table.selectRow.Type == 'ChatVoice')
        currentMusic.pause();
    }
  },
};
export default vueParam;
</script>
