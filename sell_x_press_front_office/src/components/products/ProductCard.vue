<template>
    <div class="products">
            <div class="product-container">
                <div class="product-img-container">
                    <img class="product-img" src="@/assets/categories-product-maquette-1.jpg" alt="..."/>
                </div>
                <div class="product-date">{{ publication_date }}</div>
                <div class="product-title">{{ name }}</div>
                <div class="product-description">{{ description }}</div>
                <div class="product-price">{{ price }} €</div>
                <div class="product-footer-container">
                    <div class="product-author">{{ author }}</div>
                    <button class="btn btn-primary show-more-btn">Show more</button>
                </div>
            </div>
        </div>
</template>

<script>

export default {
  name: 'ProductCard',
  props: {
    product: {
        type: Object,
        required: true,
    },
  },
  data() {
    return {
        publication_date: this.product.publication_date,
        name: this.product.name,
        description: this.product.description,
        price: this.product.price,
        author: this.product.author
    }
  },
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