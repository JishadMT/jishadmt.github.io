let blogs = [
    {
        id: "1",
        title: "Git is not picking up changes when renamed a file with upper case to lower case change?",
        description: "Ever renamed a file in Visual Studio from uppercase to lowercase, only to find Git doesn’t recognize the change? This case-insensitive quirk can disrupt your version control. Here’s a quick, foolproof way to fix it, ensuring smooth tracking across all systems.",
        tags: ['git', 'visual-studio', 'github'],
        image: "git mv.png",
        date: "09 November, 2024",
        link: "renamed-file-not-picked-by-git-changes.html",
    },
    {
        id: "2",
        title: "How to delete all files in a folder ending with specific name?",
        description: "Managing files from the Command Prompt (CMD) can be a real time-saver, especially when you need to delete multiple files with a specific naming pattern. Let's find out how.",
        tags: ['cmd', 'windows', 'file'],
        image: "del cmd.png",
        date: "09 November, 2024",
        link: "delete-files-from-command-promt-ending-with-specific-name.html",
    }
]


document.addEventListener("DOMContentLoaded", function(event) {
    loadBlogs();
});

function loadBlogs() {
    blogs.forEach(blog => {        
        let template = document.getElementById("blog-tile-template").cloneNode(true);
        template.id = `blog-tile-template-${blog.id}`;
        template.classList.remove("d-none");

        template.querySelector("#blog-title").innerHTML = blog.title;
        template.querySelector("#blog-title").href = `posts/${blog.link}`;
        template.querySelector("#blog-card-clickable").href = `posts/${blog.link}`;
        template.querySelector("#blog-description").innerHTML = blog.description;
        template.querySelector("#blog-thumbnail-container").style.backgroundImage = `url('assets/${blog.image}')`;
        template.querySelector("#blog-date").innerHTML = `${blog.date}`;

        blog.tags.forEach(tag => {
            let badgeSpan = document.createElement('span');
            badgeSpan.innerHTML = tag;
            badgeSpan.classList.add("badge", "text-bg-light", "me-2", "text-secondary");
            template.querySelector("#blog-tags").append(badgeSpan);
        });

        let blogContainer = document.getElementById("blog-list-container");
        blogContainer.append(template);
    });
}