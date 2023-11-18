const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');
const body = document.getElementById("bd");

signUpButton.addEventListener('click', () => {
	container.classList.toggle("right-panel-active");
	body.classList.toggle("body__color");
});

signInButton.addEventListener('click', () => {
	container.classList.toggle("right-panel-active");
	body.classList.toggle("body__color");
});

const inputs = document.querySelectorAll(".input");


function addcl() {
	let parent = this.parentNode.parentNode;
	parent.classList.add("focus");
}

function remcl() {
	let parent = this.parentNode.parentNode;
	if (this.value == "") {
		parent.classList.remove("focus");
	}
}


inputs.forEach(input => {
	input.addEventListener("focus", addcl);
	input.addEventListener("blur", remcl);
});
