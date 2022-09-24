//author:jxx
//此处是对表单的方法，组件，权限操作按钮等进行任意扩展(方法扩展可参照SellOrder.js)
let extension = {
  components: { //动态扩充组件或组件路径
    //表单header、content、footer对应位置扩充的组件
    gridHeader: '', //{ template: "<div>扩展组xx件</div>" },
    gridBody: '',
    gridFooter: '',
    //弹出框(修改、编辑、查看)header、content、footer对应位置扩充的组件
    modelHeader: '',
    modelBody: '',
    modelFooter: ''
  },
  buttons: {
    view: [],
    box: [],
    detail: []
  }, //扩展的按钮
  methods: { //事件扩展
    onInit() {
      this.columns.forEach(item => {
        if (item.field == "Pics") {
          //1.通过formatter自定返回图片
          var _imgs = [];
          item.formatter = (pics) => {
            (pics || "").split(",").forEach(path => {
              _imgs.push({
                name: "",
                path: path
              })
            });
            return _imgs;
          }
          //2. 如果图片是base64格式的,请将属性设置为base64=true;
          item.base64 = false;
        }
      })
    }
  }
};
export default extension;
