
function toggle(button) {
    
    if (document.getElementById("themeMode").value == "0") {
        document.getElementById("themeMode").value = "1";
        // Removes Layout Dark and Transparent Classes
        $("body").removeClass(
            "layout-transparent layout-dark bg-hibiscus bg-purple-pizzazz bg-blue-lagoon bg-electric-violet bg-portage bg-tundora bg-glass-1 bg-glass-2 bg-glass-3 bg-glass-4"
        );
        $(".sb-color-options")
            .find(".selected")
            .removeClass("selected");
        $(".sb-color-options")
            .find(".gradient-man-of-steel")
            .addClass("selected");
        // Selected Image
        //var src = $(".cz-bg-image img.sb-bg-01").attr("src");
        $(".sidebar-background").css(
            "background-image",
            "url(app-assets/img/sidebar-bg/01.jpg)"
        );
        $(".app-sidebar").css("background-image", "url(app-assets/img/sidebar-bg/01.jpg)");

        // Selected Background Color
        var bgColor = $(".cz-bg-color span.selected").attr("data-bg-color");
        $(".app-sidebar").attr("data-background-color", bgColor);
    }

    else if (document.getElementById("themeMode").value == "1") {
        document.getElementById("themeMode").value = "0";
        // Removes Unwanted Classes if any and adds layout-dark to body
        if ($("body").hasClass("layout-transparent")) {
            $("body").removeClass(
                "layout-transparent bg-hibiscus bg-purple-pizzazz bg-blue-lagoon bg-electric-violet bg-portage bg-tundora bg-glass-1 bg-glass-2 bg-glass-3 bg-glass-4"
            );
            $("body").addClass("layout-dark");
            $(".sidebar-background").css(
                "background-image",
                "url(app-assets/img/sidebar-bg/01.jpg)"
            );
            $(".app-sidebar").attr("data-background-color", "black");
        } else {
            $("body").toggleClass("layout-dark");
            $(".sb-color-options span.selected").removeClass("selected");
            $(".sb-color-options .bg-black").addClass("selected");
            $(".app-sidebar").attr("data-background-color", "black");
            $(".logo-img img").attr("src", "app-assets/img/logo.png");
        }
    }
}