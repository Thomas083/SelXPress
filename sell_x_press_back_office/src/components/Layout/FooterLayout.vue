<template>
    <footer class="footer-container">
        <div class="get_to_know_us">
            <p>Get to know us:</p>
            <a href="#">Carreer</a>
            <a href="#">About us</a>
        </div>
        <div class="want_to_buy_it">
            <p>Want to buy it ?</p>
            <a href="#">Buy on SelXPress</a>
        </div>
    </footer>
</template>
  
<script>

export default {
    name: "FooterLayout",
};
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.footer-container {
    background-color: var(--main-black);
    width: 100vw;
    display: grid;
    grid-template-columns: 25vw 50vw 25vw;
    font-family: url(../../font/Inter/static/Inter-SemiBold.ttf);
    color: var(--main-white);
    padding: 2vh 0;
}

.get_to_know_us,
.want_to_buy_it {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    font-size: 30px;
    margin-left: 5vw;
}

a {
    color: var(--main-white);
    text-decoration: none;
    font-size: 20px;
    text-align: start;
}

a:hover {
    opacity: 0.5;
    text-decoration: underline;
}

.help_us {
    display: flex;
    justify-content: center;
    align-items: center;
}

.help_us_btn {
    color: var(--main-white);
    background-color: var(--main-orange);
    border-radius: 15px;
    font-size: 25px;
    padding: 1rem;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.help_us_btn:hover {
    opacity: 0.5;
}

/* Media queries for mobile screens */
@media only screen and (max-width: 600px) {
    .footer-container {
        grid-template-columns: 1fr;
    }

    .get_to_know_us,
    .want_to_buy_it {
        align-items: center;
        margin: 1vh 0;
    }
}
</style>
  