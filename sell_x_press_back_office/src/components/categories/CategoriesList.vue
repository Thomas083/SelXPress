<template>
    <div class="categories-container">
        <div v-for="category in categories" v-on:click="sendCategoryChoose(category)">
            <div class="category">{{ category.name }}</div>
            <div class="separation"></div>
        </div>
    </div>
</template>

<script>

export default {
    name: 'CategoriesList',
    data() {
        return {
            categories: [
                {
                    id: 1,
                    name: "Ocean",
                    tags: [
                        {
                            id: 1,
                            name: "Fishing",
                            categoryId: 1
                        }
                    ]
                },
                {
                    id: 2,
                    name: "Sport",
                    tags: [
                        {
                            id: 2,
                            name: "Football",
                            categoryId: 2
                        }
                    ]
                }
            ]
        }
    },
    methods: {
        sendCategoryChoose(category) {
            this.$emit('categoryChoose', category)
        }
    },
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