<template>
    <div class="categories-container">
        <div v-for="category in categories">
            <div class="category">{{ category.name }}</div>
            <div class="separation"></div>
        </div>
    </div>
</template>

<script>
import { GET } from '@/api/axios'
import { ENDPOINTS } from '@/api/endpoints'

export default {
    name: 'CategoriesList',
    data() {
        return {
            categories: []
        }
    },
    created () {
        GET(ENDPOINTS.GET_ALL_CATEGORY, JSON.parse(localStorage.getItem('user')).token)
            .then((response) => {
                this.categories = response.data
            })
            .catch((error) => {
                console.dir(error)
            });
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