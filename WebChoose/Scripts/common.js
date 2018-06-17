(function($) {
	$(document).on('click', '.data-table .add-row', function() {
		window.location += window.createRecordUrl ? window.createRecordUrl : '/Manage';
	});
})(jQuery);
(function ($) {
	$('#bootstrap-table').on('click-row.bs.table', selectItem);

	function selectItem(row, $element, field) {
		if ($element.id) {
			window.location += '/Details/' + $element.Id;
		}
	}
})(jQuery);
(function ($) {
	$(document).on('click', '#delete-item', function () {
		swal({
				title: "Вы уверены?",
				text: "После удаления у вас не будет возможности восстановить этот объект!",
				icon: "warning",
				buttons: true,
				dangerMode: true
			})
			.then((willDelete) => {
				if (willDelete) {
					var form = $('<form action="' + $(this).data('deletelink') + '">');
					$('body').append(form);
					form.submit();
				}
			});
	});
})(jQuery);

