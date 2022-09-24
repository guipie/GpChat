'use strict';
exports.main = async (event, context) => {
	return uniCloud.downloadFile({
		fileID: event.url,
		// tempFilePath: '/tmp/test/storage/my-photo.png'
	}).then(res => {
		uniCloud.deleteFile({
			fileList: event.urls
		});
		return new Promise((resolve, reject) => {
			resolve(res);
		});
	}).catch(err => {
		return new Promise((resolve, reject) => {
			reject(new Error(err))
		});
	});
};
