document.addEventListener('DOMContentLoaded', function () {
    // Seleciona todos os botões do accordion
    var accordionItems = document.querySelectorAll('.accordion-item');
    
    accordionItems.forEach(function(item) {
        var collapseTarget = item.querySelector('.accordion-collapse');
        var button = item.querySelector('.accordion-button');
        var icon = button.querySelector('i');  // Seleciona o ícone da seta
        
        button.addEventListener('click', function() {
            // Alterna a visibilidade do conteúdo
            collapseTarget.classList.toggle('show'); 

        });
    });
});
