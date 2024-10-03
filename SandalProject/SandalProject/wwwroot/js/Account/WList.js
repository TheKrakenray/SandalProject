document.getElementById("clear-wishlist").addEventListener("click", () => {
    window.location.href="/WList/SvuotaWList/"
})


const DelButtons = document.querySelectorAll(".remove-item");
DelButtons.forEach(elem => {
    elem.addEventListener("click", () => {
        window.location.href = "/WList/EliminaWList/" + elem.id;
    });
});