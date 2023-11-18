
$(window).scroll(function () {
    if ($("#menu").offset().top > 56) {
        $("#menu").addClass("menucolor");
    } else {
        $("#menu").removeClass("menucolor");
    }
});


(function () {
    const openButton = document.querySelector('.nav__menu');
    const menu = document.querySelector('.nav__link');
    const closeMenu = document.querySelector('.nav__close');
    const logo = document.querySelector('.logo');

    openButton.addEventListener('click', () => {
        menu.classList.add('nav__link--show');
        logo.classList.add('logoone');

    });

    closeMenu.addEventListener('click', () => {
        menu.classList.remove('nav__link--show');
        logo.classList.remove('logoone');


    });




})();

(function () {
    const titleQuestions = [...document.querySelectorAll('.services__title')];
    console.log(titleQuestions)

    titleQuestions.forEach(question => {
        question.addEventListener('click', () => {
            let height = 0;
            let answer = question.nextElementSibling;
            let addPadding = question.parentElement.parentElement;

            addPadding.classList.toggle('services__padding--add');
            question.children[0].classList.toggle('services__arrow--rotate');

            if (answer.clientHeight === 0) {
                height = answer.scrollHeight;
            }

            answer.style.height = `${height}px`;
        });
    });
})();