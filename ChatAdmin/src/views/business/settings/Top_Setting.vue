<template>
  <div class="single-form">
    <Divider>内容推荐的比重占比配置</Divider>
    <VolForm ref="myform" :formFileds="formFileds" :formRules="formRules"></VolForm>
    <Button type="primary" @click="getData">当前配置</Button>
    <Button type="error" @click="reset" style="float:right;">重置</Button>
    <Button type="success" @click="save" style="float:right;margin-right:20px;">保 存</Button>
  </div>
</template>
<script>
import VolForm from "@/components/basic/VolForm.vue";
let $vue;
export default {
  components: { VolForm },
  created () {
    $vue = this;
  },
  mounted () {
    this.getData();
  },
  methods: {
    save () {
      if (this.$refs.myform.validate())
      {
        this.http.post("api/Sys_Setting/top", this.formFileds).then(res => {
          if (res)
          {
            this.$message.success("配置成功.");
            Object.assign(this.formFileds, res.data);
          }
        });
      }
    },
    getData () {
      this.http.get("api/Sys_Setting/top").then(res => {
        Object.assign(this.formFileds, res);
        this.$message.success("已获取最新配置.");
      });
    },
    reset () {
      //重置表单，重置时可指定重置的值，如果没有指定重置的值，默认全部清空
      this.$refs.myform.reset();
      this.$message.error("已重置");
    }
  },
  data () {
    return {
      formFileds: { collect: null, share: null, praise: null, xinPraise: null },
      formRules: [
        [
          {
            type: "decimal",
            title: "收藏比重",
            required: true,
            placeholder: "输入收藏在推荐所占比重.", //显示自定义的信息
            field: "collect",
            max: 100,
            colSize: 12
          }
        ],
        [
          {
            type: "decimal",
            title: "分享比重",
            required: true,
            placeholder: "输入分享在推荐所占比重.", //显示自定义的信息
            field: "share",
            max: 100,
            colSize: 12
          }
        ],
        [
          {
            type: "decimal",
            title: "新赞比重",
            required: true,
            placeholder: "输入新赞在推荐所占比重.", //显示自定义的信息
            field: "xinPraise",
            max: 100,
            colSize: 12
          }
        ],
        [
          {
            type: "decimal",
            title: "点赞比重",
            required: true,
            placeholder: "输入点赞在推荐所占比重.", //显示自定义的信息
            field: "praise",
            max: 100,
            colSize: 12
          }
        ],
        [
          {
            type: "decimal",
            title: "评论比重",
            required: true,
            placeholder: "输入评论在推荐所占比重.", //显示自定义的信息
            field: "comment",
            max: 100,
            colSize: 12
          }
        ],
      ]
    };
  }
};
</script>
<style scoped>
.single-form {
  position: relative;
  max-width: 600px;
  padding: 30px 45px;
  left: 0;
  box-shadow: #d6d6d6 0px 4px 21px;
  margin: auto;
}
</style>