'use strict';
$(document).ready(function () {

    $(document).on('click', 'a.tour', function(){
        Tour.run([
            {
                element: $('.header ul.navbar-nav li.nav-item:first'),
                content: 'این منوی همبرگری است.',
                position: 'bottom'
            },
            {
                element: $('.header .search-form form'),
                content: 'این فرم جستجو است.',
                position: 'bottom'
            },
            {
                element: $('.page-header'),
                content: 'این عنوان صفحه است.',
                position: 'bottom'
            },
            {
                element: $('.main-content .card:first'),
                content: 'این محتوای صفحه است.',
                position: 'top'
            }
		],
		{
			language: 'fa'
		});
    });

});