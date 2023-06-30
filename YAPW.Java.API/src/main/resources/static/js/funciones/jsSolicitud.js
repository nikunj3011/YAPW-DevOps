$('#modalDelete').on('show.bs.modal', function(event) {
	var enlace = $(event.relatedTarget);
	var elemento = $(event.relatedTarget);
	var href = $(elemento).attr('href');
	$('#modalDelete #btnDelete').attr('href', href);
});

$('#modalEDelete').on('hide.bs.modal	', function(event) {
	$('#modalDelete #btnDelete').attr('href', "#");
});
