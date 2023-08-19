<template>
    <div class="user-container">
        <table class="table table-striped table-hover table-bordered">
            <thead class="table-dark">
                <tr>
                    <th scope="col">Actions</th>
                    <th scope="col">Id</th>
                    <th scope="col">Name</th>
                    <th scope="col">Type</th>
                    <th scope="col">Attribute</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td scope="row" class="action-create-btn">
                        <button class="btn btn-add btn-admin" v-on:click="createAttribute">
                            Create
                            <img src="../../assets/Admin/add-category.png" alt="create" />
                        </button>
                    </td>
                    <td>-</td>
                    <td><input-component @input="createData('name', $event)" /></td>
                    <td><input-component @input="createData('type', $event)" /></td>
                    <td><img class="attribute-img" src="../../assets/Admin/attribut.png" /></td>
                </tr>
                <tr v-for="attribute in attributes">
                    <td scope="row" class="action-btns">
                        <button class="btn btn-primary btn-admin" v-on:click="sendUpdateData(attribute.id)">
                            Update
                            <img src="../../assets/Admin/bouton-modifier.png" alt="modify" />
                        </button>
                        <button class="btn btn-secondary btn-delete btn-admin">
                            Delete
                            <img src="../../assets/Admin/bouton-supprimer.png" alt="delete" />
                        </button>
                    </td>
                    <td>{{ attribute.id }}</td>
                    <td><input-component :value='attribute.name' @input="updateData(attribute.id, 'name', $event)" />
                    </td>
                    <td><input-component :value="attribute.type" @input="updateData(attribute.id, 'type', $event)" /></td>
                    <td><img class="attribute-img" src="../../assets/Admin/attribut.png" v-on:click="setSelectedAttributeIndex(attribute.id)" /></td>
                </tr>
            </tbody>
        </table>
        <table v-if="selectedAttributeIndex !== null" class="table table-striped table-hover table-bordered">
            <thead>
                <tr class="table-grey">
                    <th scope="col" colspan="4">{{ attributes[selectedAttributeIndex].name.toLocaleUpperCase() }} Value</th>
                </tr>
                <tr class="table-dark">
                    <th scope="col">Actions</th>
                    <th scope="col">Id</th>
                    <th scope="col">Name</th>
                    <th scope="col">Value</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td scope="row" class="action-create-btn">
                        <button class="btn btn-add btn-admin" v-on:click="createAttributeData(selectedAttributeIndex)">
                            Create
                            <img src="../../assets/Admin/add-category.png" alt="create" />
                        </button>
                    </td>
                    <td>-</td>
                    <td><input-component @input="createDataAttribute('name', $event)" /></td>
                    <td><input-component @input="createDataAttribute('value', $event)" /></td>
                </tr>
                <tr v-for="data in attributes[selectedAttributeIndex].data">
                    <td scope="row" class="action-btns">
                        <button class="btn btn-primary btn-admin" v-on:click="sendUpdateAttributeData(selectedAttributeIndex)">
                            Update
                            <img src="../../assets/Admin/bouton-modifier.png" alt="modify" />
                        </button>
                        <button class="btn btn-secondary btn-delete btn-admin">
                            Delete
                            <img src="../../assets/Admin/bouton-supprimer.png" alt="delete" />
                        </button>
                    </td>
                    <td>{{ data.id }}</td>
                    <td><input-component :value='data.name' @input="updateAttributeData(selectedAttributeIndex, data.id, 'name', $event)" />
                    </td>
                    <td><input-component :value="data.value" @input="updateAttributeData(selectedAttributeIndex, data.id, 'value', $event)" /></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import InputComponent from '../global/InputComponent.vue';

export default {
    name: "AttributesTab",
    components: {   
        InputComponent
    },
    data() {
        return {
            selectedAttributeIndex: null,
            formAttributeType: {
                id: null,
                name: '',
                type: '',
                data: [],
            },
            formAttributeData: {
                id: null,
                name: '',
                value: '',
            },
            attributes: [
                {
                    id: 1,
                    name: 'color',
                    type: 'select',
                    data: [
                        {
                            id: 1,
                            name: 'red',
                            value: '#FF0000'
                        },
                        {
                            id: 2,
                            name: 'green',
                            value: '#00FF00'
                        }
                    ]
                },
                {
                    id: 2,
                    name: 'size',
                    type: 'select',
                    data: [
                        {
                            id: 1,
                            name: 'small',
                            value: 'S'
                        },
                        {
                            id: 2,
                            name: 'medium',
                            value: 'M'
                        },
                        {
                            id: 3,
                            name: 'large',
                            value: 'L'
                        }
                    ]
                }
            ]
        }
    },
    methods: {
        createData(key, value) {
            this.formAttributeType = Object.assign(this.formAttributeType, { [key]: value });
        },
        createDataAttribute(key, value) {
            this.formAttributeData = Object.assign(this.formAttributeData, { [key]: value });
        },
        setSelectedAttributeIndex(index) {
            if (this.selectedAttributeIndex === index - 1) this.selectedAttributeIndex = null;
            else this.selectedAttributeIndex = index - 1;
        },
        updateData(index, key, value) {
            this.attributes[index - 1][key] = value;
        },
        updateAttributeData(index_attribute, index_data, key, value) {
            this.attributes[index_attribute].data[index_data - 1][key] = value;
        },
        sendUpdateData(index) {
            console.dir(this.attributes[index - 1])
        },
        sendUpdateAttributeData(index_attribute) {
            console.dir(this.attributes[index_attribute])
        },
        createAttribute() {
            this.formAttributeType.id = this.attributes.length + 1;
            console.dir(this.formAttributeType);
        },
        createAttributeData(index_attribute) {
            this.formAttributeData.id = this.attributes[index_attribute].data.length + 1;
            console.dir(this.formAttributeData)
        }
    },
    mounted() {

    }
}
</script>

<style scoped>
.user-container {
    padding: 1rem;
}

img {
    height: 1rem;
}

.attribute-img {
    height: 1.5rem;
    cursor: pointer;
}

.btn-admin {
    border-radius: 1rem;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
    display: flex;
    flex-direction: row;
    gap: 0.5rem;
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

.table-grey {
    height: 6rem;
    vertical-align: middle;
    font-size: 1.8rem;
    --bs-table-bg: var(--main-grey-table);
    --bs-table-color: var(--main-white)
}
</style>