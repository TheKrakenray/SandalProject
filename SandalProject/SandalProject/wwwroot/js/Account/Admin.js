const form = document.querySelector('form');
form.addEventListener('submit', (e) => {
    const fileInput = document.getElementById('file');
    if (!fileInput.files.length) {
        e.preventDefault();
        alert('Seleziona un file Excel prima di inviare il form.');
    }
});
