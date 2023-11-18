(function () {
    const titleQuestions = [...document.querySelectorAll('.questions__title_b')];
    console.log(titleQuestions)

    titleQuestions.forEach(question => {
        question.addEventListener('click', () => {
            let height = 0;
            let answer = question.nextElementSibling;
            let addPadding = question.parentElement.parentElement;

            addPadding.classList.toggle('questions__padding--add');


            if (answer.clientHeight === 0) {
                height = answer.scrollHeight;
            }

            answer.style.height = `${height}px`;
        });
    });
})();