'use strict';
exports.main = async (event, context) => {
	return uniCloud.deleteFile({
		fileList: event.urls
	});
};
