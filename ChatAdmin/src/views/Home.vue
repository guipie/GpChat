<template>
  <div class="home-contianer">
    <el-scrollbar style="height:100%;">
      <div style>
        <div data-v-542f4644 class="ivu-row">
          <div v-for="item in topColor" :key="item.name" class="ivu-col ivu-col-span-6">
            <div class="item-name">
              {{item.name}}
              <Tooltip class="icon" placement="left-start">
                <Icon type="ios-information-circle-outline" />
                <div slot="content">
                  <p>{{item.name}}</p>
                </div>
              </Tooltip>
            </div>
            <div class="total">{{(item.total+'').replace(/(\d{1,3})(?=(\d{3})+(?:$|\.))/g, "$1,")}}</div>
            <!--    <div class="rate">
              <span>
                <span>环比{{item.down}}%</span>
                <Icon class="down" type="md-arrow-dropdown" />
              </span>
              <span>
                <span>同比{{item.up}}%</span>
                <Icon class="up" type="md-arrow-dropup" />
              </span>
            </div>
            <div class="bottom">平均增长趋势{{item.up}}%</div> -->
          </div>
        </div>
        <div class="charts-line">
          <div id="charts-line" style="height:350px;"></div>
        </div>
        <div style="background:#fff; margin: 0 13px;">
          <!-- <div class="h5-desc">
            <Divider>移动H5页面(此处是H5页面,可点击--功能未实现)</Divider>
          </div> -->
          <!-- <div class="home-app">
            <div class="list">
              <Cow></Cow>
            </div>
            <div class="list">
              <Community></Community>
            </div>

            <div class="list">
              <Question></Question>
            </div>
          </div> -->
        </div>
        <div class="charts">
          <div id="charts" style="height:360px;padding-bottom:0;" class="left"></div>
          <div class="right">
            <div class="title">活跃用户榜</div>

            <div class="user-item">
              <div v-for="(item,index) in users" :key="index" class="cell">
                <div class="primary">
                  <span :class="{top3:index<3,badge:index>=3}" class="badge-count">{{index+1}}</span>
                  <el-avatar size="large" :src="http.ipAddressApp+item.avatar"></el-avatar>
                  <span class="name">{{item.nickName}}</span>
                  <span class="desc">{{item.remark}}</span>
                </div>
                <div>{{item.lastLoginDate}}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </el-scrollbar>
  </div>
</template>
<script>
  import Community from "@/../src/components/Community/index.vue";
  import Cow from "@/../src/components/Community/cow.vue";
  import Question from "@/../src/components/Community/question.vue";
  var echarts = require("echarts");

  export default {
    components: {
      Community: Community,
      Cow: Cow,
      Question: Question
    },
    data() {
      return {
        n: 90,
        users: [],
        topColor: [],
        value1: "1"
      };
    },
    mounted() {
      this.todayData();
      this.weekData();
      this.allData();
      this.userData();
    },
    methods: {
      todayData() {
        this.http.get("api/User/statistic/today").then(res => {
          this.topColor = [{
              name: "今日用户注册",
              desc: "#205",
              background: "rgb(25, 190, 107)",
              total: res.registeredCount,
              down: 0,
              up: 0,
              icon: "ios-home"
            },
            {
              name: "当前在线用户",
              desc: "#412",
              total: res.onlineCount,
              down: -15,
              up: 45,
              background: "rgb(45, 183, 245)",
              icon: "ios-help-buoy"
            },
            {
              name: "今日发布故新内容数",
              desc: "#200",
              total: res.contentCount,
              down: -18,
              up: 70,
              background: "rgb(255, 153, 0)",
              icon: "md-ionic"
            },
            {
              name: "今日发布朋友圈数",
              total: res.friendContentCount,
              down: -25,
              up: 45,
              background: "rgb(237, 64, 20)",
              icon: "ios-navigate"
            }
          ]
        });
      },
      weekData() {
        this.http.get("api/User/statistic/week").then(res => {
          var $charts_line = echarts.init(document.getElementById("charts-line"));
          $charts_line.setOption({
            title: {
              text: '本周每日数据统计'
            },
            tooltip: {
              trigger: 'axis',
              axisPointer: {
                type: 'cross',
                label: {
                  backgroundColor: '#6a7985'
                }
              }
            },

            color: ['#ffab6f', '#09b916', '#83cddc', '#e8e214'], //图例颜色
            legend: {
              icon: "roundRect",
              data: ['用户注册', '用户登录', '发布内容', '发布朋友圈']
            },
            toolbox: {
              feature: {

              }
            },
            grid: {
              left: '3%',
              right: '4%',
              bottom: '3%',
              containLabel: true
            },
            xAxis: [{
              type: 'category',
              boundaryGap: false,
              data: res.date
            }],
            yAxis: [{
              type: 'value'
            }],
            series: [{
                name: '用户注册',
                type: 'line',
                smooth: true,
                lineStyle: {
                  color: "#ffab6f",
                  width: 1
                }, //线条的样式
                data: res.data.用户注册
              },
              {
                name: '用户登录',
                type: 'line',

                smooth: true,
                lineStyle: {
                  color: "#09b916",
                  width: 1
                },
                data: res.data.用户登录
              },
              {
                name: '发布内容',
                type: 'line',

                lineStyle: {
                  color: "#83cddc",
                  width: 1
                }, //线条的样式
                smooth: true,
                data: res.data.发布内容
              },
              {
                name: '发布朋友圈',
                type: 'line',

                lineStyle: {
                  color: "#e8e214",
                  width: 1
                }, //线条的样式
                smooth: true,
                data: res.data.发布朋友圈
              }
            ]
          });
        });
      },
      allData() {
        this.http.get("api/User/statistic/all").then(res => {
          var myChart = echarts.init(document.getElementById("charts"));
          // 绘制图表
          myChart.setOption({
            color: ["#3398DB"],
            title: {
              left: "center",
              text: "数据总数统计"
            },
            tooltip: {},
            xAxis: {
              data: ["用户数", "内容数", "朋友圈数", "简介数", "评论数"]
            },
            yAxis: {},
            series: [{
              name: "数量",
              type: "bar",
              data: [res.user, res.content, res.friendContent, res.intro, res.comment]
            }]
          });
        });

      },
      userData() {
        this.http.get("api/User/statistic/user/top5").then(res => {
          this.users = res;
        });
      }
    },
  };
</script>
<style lang="less" scoped>
  .home-contianer {
    background: #efefef;
    width: 100%;
    height: 100%;
    /* padding: 20px; */
  }

  .home-app {
    display: inline-block;
    /* display: -webkit-flex;
  display: flex; */
    padding: 15px;
    padding-top: 5px;
  }

  .home-app>div {
    float: left;
    width: 33.33333%;
    padding-left: 8px;
    padding-right: 8px;
  }

  .ivu-card-body {
    text-align: center;
    padding: 25px 13px;
    padding-left: 80px;
  }

  .demo-color-name {
    color: #fff;
    font-size: 16px;
  }

  .demo-color-desc {
    color: #fff;
    opacity: 0.7;
  }

  .ivu-card {
    position: relative;
  }

  .ivu-card .icon-left {
    border-right: 1px solid;
    padding: 10px 24px;
    height: 100%;
    position: absolute;
    font-size: 50px;
    color: white;
  }

  .ivu-row {
    display: flex;
    border-bottom: 2px dotted #eee;
    padding: 0px 5px;

    .ivu-col {
      border-radius: 4px;
      background: white;
      flex: 1;
      margin: 13px 8px;
      padding: 20px 20px;
      padding-bottom: 15px;
    }

    .total {
      word-break: break-all;
      color: #282727;
      font-size: 30px;
      padding-bottom: 12px;
      font-family: -apple-system, BlinkMacSystemFont, Segoe UI, PingFang SC,
        Hiragino Sans GB, Microsoft YaHei, Helvetica Neue, Helvetica, Arial,
        sans-serif, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol;
    }

    .item-name {
      position: relative;

      .icon {
        position: absolute;
        right: 0;
        top: -1px;
      }
    }

    .item-name,
    .rate,
    .bottom {
      color: #5f5f5f;
      font-size: 14px;
    }

    .rate {
      border-bottom: 1px solid #e6e6e6;
      padding-bottom: 10px;
    }

    .bottom {
      padding-top: 8px;
    }

    .down {
      color: #10d310;
    }

    .up {
      color: red;
    }
  }

  .h5-desc {
    padding-top: 10px;
  }
</style>
<style lang="less">
  .charts-line {
    margin: 0px 13px 13px 13px;
    background: white;
    padding-top: 10px;
  }

  .charts {
    margin: 25px 13px;
    display: inline-block;
    width: 100%;

    // padding: 0px 24px;
    .left {
      padding: 25px;
      background: white;
      height: 360px;
      width: 49%;
      float: left;
      margin-right: 1%;
      background: white;
    }

    .right {
      padding: 25px 45px;
      background: white;
      height: 360px;
      width: 49%;
      float: left;
      margin-left: 1%;

      .badge-count {
        padding: 3px 7px;
        position: relative;
        border: 1px solid #eee;
        border-radius: 50%;

        margin-right: 11px;
      }

      .badge {
        background: #e2e2e2;
        color: #3a3535;
      }

      .top3 {
        background: #2db7f5;
        color: white;
      }

      .cell {
        position: relative;
        display: flex;
        padding: 10px 0;
        border-bottom: 1px dotted #eee;
      }

      .primary {
        flex: 1;
      }

      .title {
        font-size: 16px;
        padding-bottom: 6px;
        border-bottom: 1px solid #eee;
        margin-bottom: 11px;
      }

      .name {
        font-size: 15px;
        position: relative;
        top: 5px;
        color: #303133;
        left: 12px;
      }

      .desc {
        margin-left: 27px;
        font-size: 12px;
        color: #b3b3b3;
        position: relative;
        top: 5px;
      }
    }
  }
</style>
