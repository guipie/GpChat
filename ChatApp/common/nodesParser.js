import htmlParser from '@/mypUI/utils/htmlParser.js'

export default function parseHtml(val) {
	if (!val || val.trim().length === 0) {
		return []
	}
	return htmlParser(val)
}
