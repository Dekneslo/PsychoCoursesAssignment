const form = document.getElementById('form');
const username = document.getElementById('username');
const email = document.getElementById('email');
const number = document.getElementById('number');
const textarea = document.getElementById('textarea');

form.addEventListener('submit', e => {
    e.preventDefault();

    validateInputs();
});

const setError = (element, message) => {
    const inputControl = element.parentElement;
    const errorDisplay = inputControl.querySelector('.error');

    errorDisplay.innerText = message;
    inputControl.classList.add('error');
    inputControl.classList.remove('success')
}

const setSuccess = element => {
    const inputControl = element.parentElement;
    const errorDisplay = inputControl.querySelector('.error');

    errorDisplay.innerText = '';
    inputControl.classList.add('success');
    inputControl.classList.remove('error');
};

const isValidEmail = email => {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
const isValidname = username => {
    const rea = (/^[A-Za-z\s]*$/);
    return rea.test(String(username).toLowerCase());
}
const isValidnumber = number => {
    const rean = (/^[0-9]+$/);
    return rean.test(String(number).toLowerCase());
}

const validateInputs = () => {
    const usernameValue = username.value.trim();
    const emailValue = email.value.trim();
    const numberValue = number.value.trim();
    const textareaValue = textarea.value.trim();

    if (usernameValue === '') {
        setError(username, 'Username is required!');
    } else if (!isValidname(usernameValue)) {
        setError(username, 'Provide only alphabets!');
    } else {
        setSuccess(username);
    }

    if (emailValue === '') {
        setError(email, 'Email is required!');
    } else if (!isValidEmail(emailValue)) {
        setError(email, 'Provide a valid email address!');
    } else {
        setSuccess(email);
    }

    if (numberValue === '') {
        setError(number, 'Contact Number is required!');
    } else if (!isValidnumber(numberValue)) {
        setError(number, 'Provide only numbers!')
    } else {
        setSuccess(number);
    }

    if (textareaValue === 0) {
        setError(textarea, 'Message can not be empty!');
    } else {
        setSuccess(textarea);
    }

};

// change background color
function changeHeadingBg(color) {
    document.getElementById("heading").style.background = color;
}