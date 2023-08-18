<template>
    <div class="categories-tags-container">
        <table class="table table-striped table-hover table-bordered">
            <thead class="table-dark">
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">Name</th>
                    <th scope="col">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">-</th>
                    <td><input-component @input="createCatageoriesData('name', $event)" /></td>
                    <td class="action-create-btn">
                        <button class="btn btn-add btn-admin" v-on:click="createCategories">
                            Create
                            <img src="../../assets/Admin/add-category.png" alt="create" />
                        </button>
                    </td>
                </tr>
                <tr v-for="category in categories">
                    <th scope="row">{{ category.id }}</th>
                    <td><input-component :value='category.name'
                            @input="updateCategoriesData(category.id, 'name', $event)" /></td>
                    <td class="action-btns">
                        <button class="btn btn-primary btn-admin" v-on:click="sendUpdateCategoriesData(category.id)">
                            Update
                            <img src="../../assets/Admin/bouton-modifier.png" alt="modify" />
                        </button>
                        <button class="btn btn-secondary btn-delete btn-admin">
                            Delete
                            <img src="../../assets/Admin/bouton-supprimer.png" alt="delete" />
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="table table-striped table-hover table-bordered">
            <thead class="table-dark">
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">Name</th>
                    <th scope="col">CategoryId</th>
                    <th scope="col">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">-</th>
                    <td><input-component @input="createTagsData('name', $event)" /></td>
                    <td><input-component @input="createTagsData('categoryId', $event)" /></td>
                    <td class="action-create-btn">
                        <button class="btn btn-add btn-admin" v-on:click="createTags">
                            Create
                            <img src="../../assets/Admin/add-category.png" alt="create" />
                        </button>
                    </td>
                </tr>
                <tr v-for="tag in tags">
                    <th scope="row">{{ tag.id }}</th>
                    <td><input-component :value='tag.name' @input="updateTagsData(tag.id, 'name', $event)" /></td>
                    <td><input-component :value='tag.categoryId' @input="updateTagsData(tag.id, 'categoryId', $event)" />
                    </td>
                    <td class="action-btns">
                        <button class="btn btn-primary btn-admin" v-on:click="sendUpdateTagsData(tag.id)">
                            Update
                            <img src="../../assets/Admin/bouton-modifier.png" alt="modify" />
                        </button>
                        <button class="btn btn-secondary btn-delete btn-admin">
                            Delete
                            <img src="../../assets/Admin/bouton-supprimer.png" alt="delete" />
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import InputComponent from "@/components/global/InputComponent";
export default {
    name: "CategoriesTab",
    components: {
        InputComponent
    },
    data() {
        return {
            formCategoriesData: {
                id: null,
                name: '',
            },
            formTagsData: {
                id: null,
                name: '',
                categoryId: null
            },
            categories: [
                {
                    id: 1,
                    name: 'Ocean'
                },
                {
                    id: 2,
                    name: 'Sport'
                }
            ],
            tags: [
                {
                    id: 1,
                    name: 'Fishing',
                    categoryId: 1
                },
                {
                    id: 2,
                    name: 'Electric',
                    categoryId: 10
                }
            ],
        }
    },
    methods: {
        createCatageoriesData(key, value) {
            this.formCategoriesData = Object.assign(this.formCategoriesData, { [key]: value });
        },
        createTagsData(key, value) {
            this.formTagsData = Object.assign(this.formTagsData, { [key]: value });
        },
        updateCategoriesData(index, key, value) {
            this.categories[index - 1][key] = value;
        },
        updateTagsData(index, key, value) {
            this.tags[index - 1][key] = value;
        },
        sendUpdateCategoriesData(index) {
            console.dir(this.categories[index - 1])
        },
        sendUpdateTagsData(index) {
            console.dir(this.tags[index - 1])
        },
        createCategories() {
            this.formCategoriesData.id = this.categories.length + 1
            console.dir(this.formCategoriesData)
        },
        createTags() {
            this.formTagsData.id = this.tags.length + 1
            console.dir(this.formTagsData)
        }
    },
    mounted() {

    }
}
</script>

<style scoped>

.categories-tags-container {
    display: flex;
    flex-direction: row;
    gap: 2rem;
    padding: 1rem;
}

img {
    height: 1rem;
}

.action-create-btn {
  display: flex;
  justify-content: center;
}

.action-btns {
  display: flex;
  gap: 1rem;
  justify-content: center;
  align-items: center;
}

.btn-admin {
    border-radius: 1rem;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.btn-add {
    background-color: var(--main-orange);
    color: var(--main-white);
}

.btn-delete {
    background-color: var(--main-red);
    color: var(--main-white);
}

.table {
    --bs-table-border-color: var(--main-black);
}

.table-dark {
    --bs-table-bg: var(--main-green)
}
</style>