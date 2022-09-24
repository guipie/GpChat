# 检测版本升级	js sdk

**版本号:** 是指 manifest.json->基础配置->应用版本名称的值，以2个点号分隔的3组数字，如 1.0.0  2.1.9 等


## 原理

app通过post请求将app版本号发往后端服务器，与服务器上的apk版本号进行比对后，返回最新版本的url下载地址。需自行完成后端比对代码，只要返回符合下列各式的json即可（附带一个php版后端示例代码)

### POST 请求数据为 

```javascript

 	{
 		version:'数字.数字.数字 形式版本号',
 		platform:'android或ios'
 	}

```
	
### 存在新wgt 或android平台下apk，则返回
  
 ```javascript
  
  {
	    code:0
		msg:"ok"
		version:1.2.1//版本号
		url:'http...apk|wgt'//url下载地址，wgt优先，如果不存在新wgt，并且是ios，则此处为AppStore的地址
		log:''//更新文字说明，支持 \n 换行
		force:1// 是否强制升级，force=1则是强制升级，用户无法关闭升级提示框
	}
	
```

### 如果不存在新apk或wgt，则返回

```

	{
		code:1,
		msg:"no"
	}

```	
	
	
## 后端更新检测示例代码，php版本

>>
>> 	+----http://a.com/根目录	
>>		+---update.php
>>		+---apk
>>			+----1.0.1.apk
>>			+----1.0.2.apk
>>			+----1.0.3.wgt
>> 

```php
	
		<?php
		   // 在此填写ios的最新版本号及在appstore市场的url地址，用于比对是否转向去appstore市场更新
		    $ios='1.0.8';
		    $iosurl='https://appstor.apple.com/xxxxxxxxxxxxxx/xxxxxxxxxx';
		
			// 示例代码 update.php，此示例代码要求符合如下规则
			// 1. manifest.json->基础配置->应用版本名称的值必须是以2个点号分隔开的3组数字，即  "数字.数字.数字" 比如 1.0.2   2.1.3 等，不可含有其他字符，且形式必须符合 数字.数字.数字 的形式
			// 2. 该示例代码文件同目录下有 apk wgt等 文件夹，此文件夹下存放以版本号命名的apk包或wgt包，如果apk和wgt包版本号相同，则优先返回wgt，如 1.0.2.apk  1.2.4.apk 1.3.4.wgt等
			// 3. 更新检测接口地址 http://a.com/update.php, apk下载url地址 http://a.com/apk/数字.数字.数字.apk或wgt
			
			//0 不强制更新，1强制更新
			$force=0;
			//更新日志	
			$log='修复已知bug；优化性能';
			
	        // $new 存放最大的版本号，初始为 app中传来的版本号
			$new=$_POST['version'];
			
			// 存放apk或wgt的绝对路径地址
	        // 	首先判断是否有待更新的资源 wgt
			$path=__DIR__.'/apk/';
			// 此处使用了exec调用linux上系统命令返回目录下所有的apk文件名称，需要php中启用exec函数
			exec("find  {$path}  -type f -regex  '.*\(apk\|wgt\)'",$out);
			
			foreach ($out as $k=>$v){
				// 循环将最大的版本号赋给$new
				$new=getbig($new,substr(basename($v),0,-4));
			}
			header('Content-Type:application/json;charset=utf-8');
	        // 	如果版本号发生了变化，且 是热更新资源wgt，则无论ios还是android均返回资源wgt
			if($new !=$_POST['version'] && file_exists($path.$new.'.wgt')){
			    //   如果是wgt，则无论ios还是android都返回
			    echo json_encode([
					'code'=>0,
					'msg'=>'ok',
					'version'=>$new,
					'url'=>'https://exam.wonyes.org/apk/'.$new.'.wgt',
					'log'=>'新版本'.$new.','.$log,
					'force'=>$force
				]);
				exit;
			}
			
	        // 	IOS 更新判断	
			// $_POST 请求数据为 [version=>'数字.数字.数字 形式版本号',platform=>'android或ios']
			if($_POST['platform']=='ios'){
				// 如果是ios，则直接填写最新版本号和商店下载地址
				$new=$ios;//最新的版本号
				$url=$iosurl;//苹果商店地址
				// getbig($new,$_POST['version']) 函数会返回 所传入版本号参数中较大的那个
				
				if($_POST['version']!=getbig($new,$_POST['version'])){
					// 如果返回值不等于 $_POST['version']，说明 $new 是新版本
					// 返回地址
					echo json_encode([
						'code'=>0,
						'msg'=>'ok',
						'version'=>$new,
						'url'=>$url,
						'log'=>$log,
						'force'=>$force
					]);
				}else{
				    echo json_encode([
	    				'code'=>1,
	    				'msg'=>'no'
	    			]);
				}
				exit;
			}
			
	        // 	android下判断	
			// 如果 $new 无变化，则无更新
			if($new==$_POST['version']){
				echo json_encode([
					'code'=>1,
					'msg'=>'no'
				]);
				 exit;
			}else{
				echo json_encode([
						'code'=>0,
						'msg'=>'ok',
						'version'=>$new,
						'url'=>'https://exam.wonyes.org/apk/'.$new.'.apk',
						'log'=>'新版本'.$new.','.$log,
						'force'=>$force
				]);
			}
			
			
			
			// 判断2个版本号的大小，并返回较大的那个，比如 1.0.9  1.1.0 ，则返回 1.1.0
		    // 传入由2个点分隔为3组的版本号，返回较大的那个
		    // 1.0.3  1.2.9 等
		    function getbig($one,$two){
		        $onearr=explode('.',$one);
		        $twoarr=explode('.',$two);
		        if(intval($onearr[0]) !== intval($twoarr[0])){
		            return intval($onearr[0])>intval($twoarr[0])?$one:$two;
		        }
		        
		        if(intval($onearr[1]) !== intval($twoarr[1])){
		            return intval($onearr[1])>intval($twoarr[1])?$one:$two;
		        }
		        if(intval($onearr[2]) !== intval($twoarr[2])){
		            return intval($onearr[2])>intval($twoarr[2])?$one:$two;
		        }
		        return $one;
		    }
	
```

## 使用方法

1. 将插件下载后，common 目录直接覆盖在项目根目录下或者直接用HBuilderX导入
2. 在App.vue里顶端 <script> 后引入下面代码
	
	```javascript
	
	// 如果下载的，并放在了common目录下
	
	// #ifdef APP-PLUS
		import checkappupdate from 'common/checkappupdate.js'
	// #endif
	
	
	
	// 如果导入的
	
	// #ifdef APP-PLUS
	import checkappupdate from 'js_sdk/wonyes-checkappudate/checkappupdate.js'
	// #endif
	
	
	```
3. 同样在App.vue 里 onLaunch: 方法里，引入如下代码

	```
	
	//调用示例 配置参数， 默认如下，其中api是接口地址，必须填写
	
	// #ifdef APP-PLUS
	checkappupdate.check({
		title:"检测到有新版本！",
		content:"请升级app到最新版本！",
		canceltext:"暂不升级",
		oktext:"立即升级",
		api:'http://exam.wonyes.org/update.php',
		barbackground:"rgba(50,50,50,0.8)",//进度条背景色，默认灰色，可自定义rgba形式色值
		barbackgroundactive:"rgba(32,165,58,1)"//进度条前景色色，默认绿色，可自定义rgba形式色值
	})
	// #endif
	
	```
4. 如果使用示例php代码，则按下述配置，其他方式参考此配置
	
	A. 假设域名为 a.com  更新接口文件为update.php，接口地址为 http://a.com/update.php
	B. 复制示例代码，粘贴到update.php中
	C. 在update.php同目录下建立apk文件夹，里面存放应用的apk包或wgt包，包命名必须为 版本号.apk  .wgt，即符合 “数字.数字.数字.wgt|apk”形式,如果存在同版本号的apk和wgt，则wgt优先

5. 确保站点绑定解析均正常，到此完成


## 演示app

![安装演示app](https://exam.wonyes.org/apk.png)



## 使用示例项目

如果你通过文档说明还无法顺利使用，可下载示例项目，查看应用过程




## 如果觉得有用，不妨赞助一下，多少随意 ┑(￣Д ￣)┍

![](https://www.wonyes.org/wx.jpg)

![](https://www.wonyes.org/alipay.jpg)

