// Js for input type file
var inputs = document.querySelectorAll('.inputfile');
Array.prototype.forEach.call(inputs, function (input) {
	var label = input.nextElementSibling,
		labelVal = label.innerHTML;

	input.addEventListener('change', function (e) {
		var fileName = '';
		if (this.files && this.files.length > 1) {
			fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
		}else {
			fileName = e.target.value.replace(/^.*[\\\/]/, '');
		}
		console.log(fileName);
		if (fileName != "") {
			console.log(label);
			label.innerText = fileName;
		}else { 
			label.innerHTML = labelVal;
		}
	});
});
