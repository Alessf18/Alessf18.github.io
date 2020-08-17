"use strict";

// Class definition
var KTDualListbox = function () {

	// Private functions
	var initDualListbox = function () {
        // Dual Listbox
        var listBoxes = $('.kt-dual-listbox');

        listBoxes.each(function(){
            var $this = $(this);
            var id = '#' + $this.attr('id');
            // get titles
            var availableTitle = ($this.attr('data-available-title') != null) ? $this.attr('data-available-title') : 'Opciones disponibles';
            var selectedTitle = ($this.attr('data-selected-title') != null) ? $this.attr('data-selected-title') : 'Opciones seleccionadas';

            // get button labels
            var addLabel = ($this.attr('data-add') != null) ? $this.attr('data-add') : 'Agregar';
            var removeLabel = ($this.attr('data-remove') != null) ? $this.attr('data-remove') : 'remover';
            var addAllLabel = ($this.attr('data-add-all') != null) ? $this.attr('data-add-all') : 'Agregar Todos';
            var removeAllLabel = ($this.attr('data-remove-all') != null) ? $this.attr('data-remove-all') : 'Remover Todos';

            // get options
            var options = [];
            $this.children('option').each(function(){
                var value = $(this).val();
                var label = $(this).text();
                var selected = ($(this).is(':selected')) ? true : false;
                options.push({ text: label, value: value, selected: selected });
			}); 
			
			// get search option
			var search = ($this.attr('data-search') != null) ? $this.attr('data-search') : "";

            // clear duplicates
            $this.empty();

            // init dual listbox
            var dualListBox = new DualListbox(id,{
                addEvent: function(value) {
                    console.log(value);
                },
                removeEvent: function(value) {
                    console.log(value);
                },
                availableTitle: availableTitle,
                selectedTitle: selectedTitle,
                addButtonText: addLabel,
                removeButtonText: removeLabel, 
                addAllButtonText: addAllLabel,
                removeAllButtonText: removeAllLabel,
                options: options
			});
			
			if (search == "false"){
				dualListBox.search.classList.add('dual-listbox__search--hidden');
			}
        }); 
	}

	return {
		// public functions
		init: function() {
			initDualListbox();
		}
	};
}();

KTUtil.ready(function() {	
	KTDualListbox.init();
});