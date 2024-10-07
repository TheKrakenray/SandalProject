//const items = document.querySelectorAll(".cart-item");
//items.forEach(item => {
//    var delButton = document.getElementById("remove-item_" + item.id);
//    var increaseButton = document.getElementById("increase_" + item.id);
//    var decreaseButton = document.getElementById("decrease_" + item.id);
//    var quantity = document.getElementById("quantity_" + item.id);

//    increaseButton.addEventListener("click", () => {
//        quantity.value++;
//    })
//    decreaseButton.addEventListener("click", () => {
//        if(quantity.value > 1)
//            quantity.value--;
//    })
//});

function ApplicaSconto() {
    var codice = document.getElementById("promo-code").value;
    var prezzo = document.getElementById("prezzo");
    var sconto = document.getElementById("sconto");
    if (codice == "HXDZ-Z043-DEFQ-LMK5") {
        prezzo.innerHTML = parseInt(prezzo.innerHTML.replace("€", "")) - (parseInt(prezzo.innerHTML.replace("€", "")) / 10) + "€";
        sconto.removeAttribute("style");
    }
}