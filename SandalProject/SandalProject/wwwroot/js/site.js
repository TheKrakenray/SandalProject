var isDarkMode;

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
    const container = document.querySelector('.container');

    const defaultModeIsDark = localStorage.setItem('defaultModeIsDark', window.matchMedia("(prefers-color-scheme: dark)").matches)


        isDarkMode = localStorage.getItem('isDarkMode');
        

        if (isDarkMode == "true") {
            darkModeCheckboxFooter.checked = true;
            darkModeCheckbox.checked = true;
            
            if(container!=null)
            container.classList.add('dark-mode');

            document.body.classList.add('dark-mode');
            header.classList.add('dark-mode');
            footer.classList.add('dark-mode');
            menuPopup.classList.add('dark-mode');
            darkModeLabel.classList.add('dark-mode');
            menuItems.forEach(item => item.classList.add('dark-mode'));
            const loginPopup = document.querySelector('.login-popup');
            if (loginPopup) {
                loginPopup.classList.add('dark-mode');
            }


        }
        else {
            darkModeCheckboxFooter.checked = false;
            darkModeCheckbox.checked = false;
            
            if (container != null)
            container.classList.remove('dark-mode');

            document.body.classList.remove('dark-mode');
            header.classList.remove('dark-mode');
            footer.classList.remove('dark-mode');
            menuPopup.classList.remove('dark-mode');
            darkModeLabel.classList.remove('dark-mode');
            menuItems.forEach(item => item.classList.remove('dark-mode'));
            const loginPopup = document.querySelector('.login-popup');
            if (loginPopup) {
                loginPopup.classList.remove('dark-mode');
            }
        }




    function toggleDarkMode(checked) {
        if (container != null)
            container.classList.toggle('dark-mode', checked);

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

        localStorage.setItem('isDarkMode', checked);
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
    

  //------------------------------------------------------------------------------//
 //------------------------------MENU BUTTONS------------------------------------//
//------------------------------------------------------------------------------//

document.getElementById("search-input").addEventListener("keypress", function (k) {
    if (k.key === "Enter") {
    var textInput = document.getElementById("search-input").value;
    window.location.href = "/Elenco/Risultati?ricerca="+textInput;
    }
    console.log(k.key);
});

document.getElementById("wlist").addEventListener("click", () => {
        window.location.href = "/WList/WList/";
});
document.getElementById("cart").addEventListener("click", () => {
        window.location.href = "/Carrello/Carrello/";
});
document.getElementById("user").addEventListener("click", () => {
        window.location.href = "/Utente/Login/";
});


document.getElementById("menu-item-Uomo").addEventListener("click",() => {
    window.location.href = "/Elenco/Genere?ricerca=uomo";
});
document.getElementById("menu-item-Donna").addEventListener("click",() => {
    window.location.href = "/Elenco/Genere?ricerca=donna";
});
document.getElementById("menu-item-Bambino").addEventListener("click",() => {
    window.location.href = "/Elenco/Genere?ricerca=bambino";
});



document.getElementById("menu-item-Primavera").addEventListener("click", () => {
    window.location.href = "/Elenco/Categoria?ricerca=primavera";
});
document.getElementById("menu-item-Estate").addEventListener("click", () => {
    window.location.href = "/Elenco/Categoria?ricerca=estate";
});
document.getElementById("menu-item-Autunno").addEventListener("click", () => {
    window.location.href = "/Elenco/Categoria?ricerca=autunno";
});
document.getElementById("menu-item-Inverno").addEventListener("click", () => {
    window.location.href = "/Elenco/Categoria?ricerca=inverno";
});


document.getElementById("menu-item-AboutUs").addEventListener("click", () => {
    window.location.href = "/Info/AboutUs/";
});
document.getElementById("menu-item-FAQ").addEventListener("click", () => {
    window.location.href = "/Info/FAQ/";
});
document.getElementById("menu-item-Contacts").addEventListener("click", () => {
    window.location.href = "/Info/Contatti/";
});



//--COOKIES--//

window.onload = function () {
    if (!localStorage.getItem("cookiesAccepted")) {
        document.getElementById("cookie-popup").style.display = "flex";
    }

    document.getElementById("accept-cookies").addEventListener("click", function () {
        localStorage.setItem("cookiesAccepted", "true");
        document.getElementById("cookie-popup").style.display = "none";
        document.getElementById("cookie-popup").remove();
    });
};

document.getElementById('accept-cookies').addEventListener('click', function () {
    document.getElementById('cookie-popup').classList.add('hidden');
});

if (localStorage.getItem('cookiesAccepted') === 'true') {
    document.getElementById('cookie-popup').classList.add('hidden');
}

document.getElementById('search').addEventListener('click', function () {
    var searchInput = document.getElementById('search-input');
    if (searchInput.style.display === 'none' || searchInput.style.display === '') {
        searchInput.style.display = 'block';
        searchInput.focus();
    } else {
        searchInput.style.display = 'none';
    }
});