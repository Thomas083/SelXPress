<template>
    <div class="products">
            <div class="product-container">
                <div class="product-img-container">
                    <img class="product-img" src="@/assets/categories-product-maquette-1.jpg" alt="..."/>
                </div>
                <div class="product-date">20/06/2023</div>
                <div class="product-title">SAMODA, Child, Baleine, T-Shirt</div>
                <div class="product-description">T-Shirt for youngest with small whale, Available in sizes 6 to 1...</div>
                <div class="product-price">16,99 €</div>
                <div class="product-footer-container">
                    <div class="product-author">SAMODA</div>
                    <button class="btn btn-primary show-more-btn">Show more</button>
                </div>
            </div>
        </div>
</template>

<script>

export default {
  name: 'ProductCard',
}

</script>

<style scoped>

.product-container {
    display: flex;
    flex-direction: column;
    background-color: var(--main-white);
    align-items: flex-start;
    border-radius: 10px;
    padding: 1rem;
    max-width: 300px;
}

.product-img-container {
    width: 100%;
}
.product-img {
    width: 197px;
    height: 173px;
}

.product-date {
    color: var(--main-grey-product);
    text-align: right;
    width: 100%;
    margin-top: 0.5rem;
    margin-bottom: 0.5rem;
}

.product-title {
    font-weight: bold;
    word-wrap: break-word;
}

.product-description {
    word-wrap: break-word;
    text-align: justify;
}


.product-price {
    font-weight: bold;
    font-size: 2rem;
}

.product-footer-container {
    display: flex;
    justify-content: space-between;
    align-items: flex-end;
    width: 100%;
}

.product-author {
    color: var(--main-grey-product);
    text-decoration: underline;
    cursor: pointer;
}

.product-author:hover {
    opacity: 0.7;
}

.show-more-btn {
    border-radius: 2rem;
}

</style>