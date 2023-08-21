<template>
    <form class="form-select-input" @submit.prevent="sendsearchdata">
        <select class="form-select" aria-label="Default select example" v-model="searchFormData.type">
            <option value="name">Name</option>
            <option value="price">Price</option>
            <option value="publication_date">Date</option>
        </select>
        <div class="search-input">
            <input type="text" placeholder="'Search your product...'" v-model="searchFormData.value" />
            <button class="btn btn-primary" @click="SendSearchData()">Search</button>
        </div>
    </form>
</template>

<script>
import InputComponent from "@/components/global/InputComponent.vue";

export default {
    name: 'FilterProduct',
    components: {
        InputComponent,
    },
    data() {
        return {
            searchFormData: {
                type: 'name',
                value: ''
            }
        }
    },
    methods: {
        SendSearchData() {
            console.dir(this.searchFormData)
        }
    }
}
</script>

<style scoped>
.form-select-input {
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 1rem;
    height: 70%;
}
.search-input {
    display: flex;
    align-items: center;
    position: relative;
    gap: 0.5rem
}
</style>