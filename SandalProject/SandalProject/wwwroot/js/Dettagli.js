function toggleHeart() {
    var heartIcon = document.getElementById("heart-icon");
    if (heartIcon.classList.contains("far")) {
        heartIcon.classList.remove("far");
        heartIcon.classList.add("fas");
    } else {
        heartIcon.classList.remove("fas");
        heartIcon.classList.add("far");
    }
}

const pointerScroll = (elem) => {

    const dragStart = (ev) => elem.setPointerCapture(ev.pointerId);
    const dragEnd = (ev) => elem.releasePointerCapture(ev.pointerId);
    const drag = (ev) => elem.hasPointerCapture(ev.pointerId) && (elem.scrollLeft -= ev.movementX);

    elem.addEventListener("pointerdown", dragStart);
    elem.addEventListener("pointerup", dragEnd);
    elem.addEventListener("pointermove", drag);
};

/* document.querySelectorAll(".colore").forEach(pointerScroll); */


window.onload = () => {
    const colori = document.getElementById("selected");
    const bg_color = colori.style.getPropertyValue('--coloreSandalo');
    console.log(bg_color);
    if (bg_color == 'black' || bg_color.includes('#000000') || bg_color.includes('#111111') || bg_color.includes('#222222') || bg_color.includes('#333333'))
        colori.style.border = '0.2vw solid cyan';
    else
        colori.style.border = '0.2vw solid black';
}