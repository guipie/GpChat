import CryptoJS from 'crypto-js'
var aseKey = CryptoJS.enc.Utf8.parse("guxin_app_716716"); //秘钥必须为：8/16/32位  
//加密
export const encrypt = (message) => CryptoJS.AES.encrypt(message, aseKey, {
  mode: CryptoJS.mode.ECB,
  padding: CryptoJS.pad.Pkcs7
}).toString();

//解密
export const decrypt = (message) => CryptoJS.AES.decrypt(message, aseKey, {
  mode: CryptoJS.mode.ECB,
  padding: CryptoJS.pad.Pkcs7
}).toString(CryptoJS.enc.Utf8);