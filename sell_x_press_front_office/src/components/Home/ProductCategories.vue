<template>
        <div id="carouselExample" class="carousel carousel-dark slide" data-bs-ride="carousel">
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
                </div>
                <div class="carousel-item">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
                </div>
                <div class="carousel-item">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
                    <img src="../../assets/Tests/categories-product-maquette-1.jpg" alt="...">
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
    <div class="products-by-categories-container">
        <div class="title-show-more-container">
            <h2>Ocean and Waters</h2>
            <button class="show-more-btn">Show more</button>
        </div>
        <div class="products-container">
            <img src="../../assets/Tests/categories-product-maquette-1.jpg" />
            <img src="../../assets/Tests/categories-product-maquette-1.jpg" />
            <img src="../../assets/Tests/categories-product-maquette-1.jpg" />
            <img src="../../assets/Tests/categories-product-maquette-1.jpg" />
            <img src="../../assets/Tests/categories-product-maquette-1.jpg" />
        </div>
    </div>
</template>

<script>

export default {
    name: 'ProductCategories'
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

.products-by-categories-container {
    background-color: var(--main-white);
    margin: 1rem;
    border: none;
    border-radius: 10px;
    display: flex;
    flex-direction: column;
}
.title-show-more-container {
    display: flex;
    flex-direction: row;
    gap: 1rem;
    padding: 1rem;
    color: var(--main-black);
}

.show-more-btn {
    border: none;
    background-color: transparent;
    color: var(--main-grey-product);
}

.show-more-btn:hover {
    text-decoration: underline;
    opacity: 0.7;
}

.products-container {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    padding: 1rem;
    gap: 10vw;
}

.product {
    padding: 1rem;
}

img {
    height: 20vh;
    cursor: pointer;
}
</style>