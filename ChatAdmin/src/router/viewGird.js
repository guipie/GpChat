let viewgird = [
  {
    path: '/Sys_Log',
    name: 'sys_Log',
    component: () => import('@/views/system/Sys_Log.vue')
  }
  , {
    path: '/Sys_User',
    name: 'Sys_User',
    component: () => import('@/views/system/Sys_User.vue')
  }, {
    path: '/Sys_Dictionary',
    name: 'Sys_Dictionary',
    component: () => import('@/views/system/Sys_Dictionary.vue')
  }, {
    path: '/Sys_Role',
    name: 'Sys_Role',
    component: () => import('@/views/system/Sys_Role.vue')
  }
  , {
    path: '/Sys_DictionaryList',
    name: 'Sys_DictionaryList',
    component: () => import('@/views/system/Sys_DictionaryList.vue')
  }, {
    path: '/Content_File',
    name: 'Content_File',
    component: () => import('@/views/business/content/Content_File.vue')
  }, {
    path: '/Content',
    name: 'Content',
    component: () => import('@/views/business/content/Content.vue')
  }, {
    path: '/Recommend_Setting',
    name: 'Recommend_Setting',
    component: () => import('@/views/business/settings/Recommend_Setting.vue')
  }, {
    path: '/Top_Setting',
    name: 'Top_Setting',
    component: () => import('@/views/business/settings/Top_Setting.vue')
  }, {
    path: '/Content_Intro',
    name: 'Content_Intro',
    component: () => import('@/views/business/contentintro/Content_Intro.vue')
  }, {
    path: '/Notice',
    name: 'User_Sys_Notice',
    component: () => import('@/views/business/Notice/User_Sys_Notice.vue')
  }, {
    path: '/Sys_Agreement',
    name: 'Sys_Agreement',
    component: () => import('@/views/business/settings/Sys_Agreement.vue')
  }, {
    path: '/Complaint',
    name: 'Complaint',
    component: () => import('@/views/system/Complaint.vue')
  }
  , {
    path: '/App_Upgrade',
    name: 'App_Upgrade',
    component: () => import('@/views/system/App_Upgrade.vue')
  }
  , {
    path: '/chats',
    name: 'chats',
    component: () => import('@/views/system/GuXin_Chat_Record.vue')
  }, {
    path: '/bt',
    name: 'bt',
    component: () => import('@/views/system/Bt.vue')
  }]
export default viewgird
