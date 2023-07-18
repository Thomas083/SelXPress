<template>
    <div class="categories-container">
            <div class="category">Category#1</div>
            <div class="separation"></div>
            <div class="category">Category#2</div>
            <div class="separation"></div>
            
        </div>
</template>

<script>

export default {
    name: 'CategoriesList'
}

</script>

<style scoped>

.categories-container {
    display: flex;
    flex-direction: column;
    background-color: var(--main-white);
    gap: 0.5rem;
    border-radius: 15px;
    padding: 1rem;
    height: max-content;
    max-width: 300px;
    align-items: flex-start;
}

.category {
    font-size: 2rem;
    cursor: pointer;
}

.category:hover {
    opacity: 0.7;
}

.separation {
    border: 1px var(--main-grey-product) solid;
    width: 90%;
}

</style>