<template>
    <div class="products">
            <div class="product-container">
                <div class="product-date">{{product.publication_date}}</div>
                <div class="product-title">{{product.name}}</div>
                <div class="product-ref">Ref : {{ product.id }}</div>
                <div class="product-price">{{ product.price }} €</div>
                <div class="product-footer-container">
                    <button class="btn btn-primary update-btn">Update <img class="modify-img" src="@/assets/Card/modify.png" alt="..." /></button>
                    <button class="btn btn-primary delete-btn">Delete <img class="delete-img" src="@/assets/Card/bouton-supprimer.png" alt="..." /></button>
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
}

.product-ref {
    color: var(--main-grey-product);
    text-align: start;
    width: 100%;
    margin-top: 0.5rem;
    margin-bottom: 0.5rem;
}

.product-title {
    font-weight: bold;
    word-wrap: break-word;
}

.product-price {
    font-weight: bold;
    font-size: 2rem;
}

.product-footer-container {
    display: flex;
    justify-content: flex-end;
    align-items: flex-end;
    gap: 1rem;
    width: 100%;
    margin-top: 1rem;
}

.modify-img,
.delete-img {
width: 14px;
height: 14px;
}

.product-author {
    color: var(--main-grey-product);
    text-decoration: underline;
    cursor: pointer;
}

.product-author:hover {
    opacity: 0.7;
}

.update-btn,
.delete-btn {
    display: flex;
    align-items: center;
    gap: 0.5rem;
    border-radius: 1rem;
}

.delete-btn {
    background-color: var(--main-red);
    --bs-btn-active-bg: var(--main-red)
}

.delete-btn:hover {
    background-color: var(--main-red);
    opacity: 0.7;
}

</style>