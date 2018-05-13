(function($) {
	$.fn.extend({
		serializeToObject: function () {
			return this.map(function () {
				var config = {};
				$(this).serializeArray().map(function (item) {
					if (config[item.name]) {
						if (typeof (config[item.name]) === "string") {
							config[item.name] = [config[item.name]];
						}
						config[item.name].push(item.value);
					} else {
						config[item.name] = item.value;
					}
				});
				return config;
			});
		}
	});
})(jQuery);
(function (moment, $) {
	function createBootstrapTable() {
		$('[id^=bootstrap-table')
			.bootstrapTable({
				striped: true
			});
	}

	$.extend($.fn.bootstrapTable.defaults, $.fn.bootstrapTable.locales['ru-RU']);

	createBootstrapTable();

	moment.locale('ru');

})(moment, jQuery);