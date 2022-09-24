//author:jxx
//此处是对表单的方法，组件，权限操作按钮等进行任意扩展(方法扩展可参照SellOrder.js)

let extension = {
  components: {//动态扩充组件或组件路径
    //表单header、content、footer对应位置扩充的组件
    gridHeader: '',//{ template: "<div>扩展组xx件</div>" },
    gridBody: '',
    gridFooter: '',
    //弹出框(修改、编辑、查看)header、content、footer对应位置扩充的组件
    modelHeader: '',
    modelBody: '',
    modelFooter: ''
  },
  buttons: { view: [], box: [], detail: [] },//扩展的按钮
  methods: {//事件扩展
    onInit () {
      this.columns.forEach(column => {
        //修改颜色
        if (column.field == 'Type')
        {
          column.formatter = (row) => {
            if (row.Type == 'ChatImage')
              return '<span style="color: #2d8cf0;">图片</span>'
            else if (row.Type == 'ChatVoice')
              return '<span style="color:#38d777;">语音</span>'
            else
              return '<span style="color: #d54069;">文字</span>'
          }
        }
      });
      this.columns.push({
        title: '操作', width: 90, render: (h, { row, column, index }) => {
          return h(
            "div",
            { style: {} },
            [
              h(
                "Button",
                {
                  props: { type: "success", size: "small" },
                  style: {},
                  on: {
                    click: (e) => {
                      e.stopPropagation();
                      this.table.dialogVisible = true;
                      this.table.selectRow = row;
                    }
                  }
                },
                "查看消息"
              )
            ]
          );
        }
      });
    }
  }
};
export default extension;