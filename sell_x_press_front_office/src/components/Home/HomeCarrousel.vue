<template>
    <div id="carouselExample" class="carousel carousel-dark slide" data-bs-ride="carousel">
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                </div>
                <div class="carousel-item">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                </div>
                <div class="carousel-item">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/categories-product-maquette-1.jpg" alt="...">
                </div>
            </div>
            <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Previous</span>
            </button>
            <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Next</span>
            </button>
        </div>
</template>

<script>
    export default {
        
    }
</script>

<style scoped>

.carousel-item.active {
    display: flex;
    flex-direction: row;
    justify-content: center;
    flex-wrap: wrap;
    gap: 10vw;
}

.carousel {
    background-color: var(--main-white);
    margin: 5rem;
    padding: 2rem 0;
    border: none;
    border-radius: 10px;
    display: flex;
    flex-direction: column;
}

img {
    height: 20vh;
    cursor: pointer;
}

</style>