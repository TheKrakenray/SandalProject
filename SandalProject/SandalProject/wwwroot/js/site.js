document.addEventListener('DOMContentLoaded', function () {
    const menuButton = document.querySelector('.menu-button');
    const closeButton = document.querySelector('.close-button');
    const closeText = document.querySelector('.close-text');
    const overlay = document.querySelector('.overlay');
    const menuPopup = document.querySelector('.menu-popup');
    const darkModeCheckbox = document.getElementById('dark-mode-checkbox');
    const darkModeCheckboxFooter = document.getElementById('dark-mode-checkbox-footer');
    const darkModeLabel = document.querySelector('.dark-mode-label');
    const menuItems = document.querySelectorAll('.menu-item');
    const header = document.querySelector('.header');
    const footer = document.querySelector('footer');

    function toggleDarkMode(checked) {
        document.body.classList.toggle('dark-mode', checked);
        header.classList.toggle('dark-mode', checked);
        footer.classList.toggle('dark-mode', checked);
        menuPopup.classList.toggle('dark-mode', checked);
        darkModeLabel.classList.toggle('dark-mode', checked);
        menuItems.forEach(item => item.classList.toggle('dark-mode', checked));

        const loginPopup = document.querySelector('.login-popup');
        if (loginPopup) {
            loginPopup.classList.toggle('dark-mode', checked);
        }
    }

    darkModeCheckbox.addEventListener('change', function () {
        toggleDarkMode(this.checked);
        darkModeCheckboxFooter.checked = this.checked;
    });

    darkModeCheckboxFooter.addEventListener('change', function () {
        toggleDarkMode(this.checked);
        darkModeCheckbox.checked = this.checked;
    });

    menuButton.addEventListener('click', function () {
        overlay.classList.add('active');
        menuPopup.classList.add('active');
        menuPopup.classList.remove('inactive');
    });

    function closePopup() {
        menuPopup.classList.add('inactive');
        setTimeout(function () {
            overlay.classList.remove('active');
            menuPopup.classList.remove('active');
        }, 300);
    }

    closeButton.addEventListener('click', closePopup);
    closeText.addEventListener('click', closePopup);
    overlay.addEventListener('click', closePopup);
});

