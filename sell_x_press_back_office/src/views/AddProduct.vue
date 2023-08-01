<template>
    <div class="add-product-container">
        <div class="img-details-container">
            <div class="import-file-container">
                <div class="img-upload" id="input-upload-file" name="input-upload-file">
                    <img ref="uploadedImage" :src="formData.upload_file" alt="Uploaded Image" />
                </div>
                <div class="upload-file" @click="selectFile">
                    <p>Import file</p>
                    <img src="@/assets/Product/upload.png" alt="..." />
                    <input id="file-input" type="file" style="display: none" @change="handleFileSelection" />
                </div>
            </div>
            <div :onChange="$emit('inputForm', formData)" class="product-details">
                <input-component label="Title :" id="input-title" name="input-title" type="text"
                    placeholder="Enter your product title..." @input="updateData($event, 'title')" />
                <input-component label="Price :" id="input-price" name="input-price" type="text" placeholder="...€"
                    @input="updateData($event, 'price')" />
                <div v-for="(chooseOption, index_attribute) in chooseOptions" :key="index" class="attributes">
                    <div class="attributes-data">
                        <select class="form-select" v-model="chooseOption.attribute.name"
                            @change="setAttributeName($event.target.value)">
                            <option v-for="option in selectOptions" :key="option" :value="option">
                                {{ option }}
                            </option>
                        </select>
                        <div v-for="(data, index_value) in chooseOption.attribute.values">
                            <select v-if="chooseOptions[index_attribute].attribute.name === 'Color'" v-model="data.value"
                                @change="setAttributeData(index_attribute, index_value)" class="form-select"
                                :style="{ backgroundColor: data.value }">
                                <option v-for="option in selectColorData" :key="option.name" :value="option.value"
                                    class="option-color">
                                    {{ option.name }}
                                </option>
                            </select>
                        </div>
                        <div class="add-attributes" @click="addAttributes(index_attribute)">+ Add Data</div>
                    </div>
                </div>
                <div class="add-attributes" @click="addSelect()">+ Add Attributes</div>
            </div>
        </div>
        <div class="separation-line"></div>
        <div class="description-container">
            <h1>Description</h1>
            <div :onChange="$emit('inputForm', formData)">
                <textarea class="product-description" rows="10" cols="100" id="input-description" name="input-description"
                    placeholder="Write the product description here..."
                    @change="updateData($event.target.value, 'description')"></textarea>
            </div>
        </div>
        <div class="save-btn-container">
            <button class="btn btn-primary save-btn" @click="addProduct">Add Product</button>
        </div>
    </div>
</template>

<script>

import InputComponent from "@/components/global/InputComponent.vue";
export default {
    name: "AddProduct",
    components: {
        InputComponent,
    },
    data() {
        return {
            formData: {
                title: "",
                price: "",
                description: "",
                upload_file: "",
                attributes: [],
            },
            // attributes: [
            //     {
            //         name: 'color',
            //         type: 'select',
            //         data: [
            //             { name: '--- Select a color ---', value: '' },
            //             { name: 'blue', value: '#0000FF' },
            //             { name: 'red', value: '#FF0000' },
            //             { name: 'green', value: '#00FF00' }
            //         ]
            //     },
            //     {
            //         name: 'size',
            //         type: 'select',
            //         data: [
            //             { name: '--- Select a size ---', value: '' },
            //             { 'name': 'extra_small', 'value': 'xs' },
            //             { 'name': 'small', 'value': 's' },
            //             { 'name': 'medium', 'value': 'm' },
            //             { 'name': 'large', 'value': 'l' },
            //             { 'name': 'extra_large', 'value': 'xl' }
            //         ]
            //     }
            // ],
            selectOptions: ['--- Select a type ---', 'Color', 'Size'],
            selectColorData: [
                { name: '--- Select a color ---', value: '' },
                { name: 'blue', value: '#0000FF' },
                { name: 'red', value: '#FF0000' },
                { name: 'green', value: '#00FF00' }
            ],
            selectedColors: [],
            chooseOptions: [],
        };
    },
    methods: {
        updateData(e, key) {
            this.formData = Object.assign(this.formData, { [key]: e });
        },
        selectFile() {
            document.getElementById('file-input').click();
        },
        handleFileSelection(event) {
            const selectedFile = event.target.files[0];
            if (selectedFile) {
                // Convertir le fichier en URL de données (data URL) pour l'afficher
                const reader = new FileReader();
                reader.onload = () => {
                    this.formData.upload_file = reader.result;
                };
                reader.readAsDataURL(selectedFile);
            }
        },
        addSelect() {
            this.chooseOptions.push({ attribute: { name: '--- Select a type ---', values: [{ name: '', value: '' }] } })
        },
        addAttributes(index) {
            this.chooseOptions[index].attribute.values.push({ name: '', value: '' })
        },
        setAttributeName(e) {
            if (e && !this.formData.attributes.some(attr => attr.name === e)) {
                this.formData.attributes.push({ name: e, data: [] });
            }
        },
        setAttributeData(index_attribute, index_value) {
            console.dir(this.chooseOptions[index_attribute])
            const value = this.chooseOptions[index_attribute].attribute.values[index_value].value;
            if (value) {
                const attributeName = this.chooseOptions[index_attribute].attribute.name;
                const attributeIndex = this.formData.attributes.findIndex(attr => attr.name === attributeName);
                if (attributeIndex !== -1) {
                    this.formData.attributes[attributeIndex].data.push({name: attributeName, value: value});
                }
            }
        },
        addProduct() {
            console.dir(this.formData)
        }
    },
};

</script>

<style scoped>
.add-product-container {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    padding: 3rem;
    margin: 1rem;
    background-color: var(--main-white);
    border-radius: 1rem;
}

.img-details-container {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    gap: 1rem;
}

.separation-line {
    border: 1px var(--main-grey-separation) solid;
    width: 100%;
}

.product-details {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}


.description-container {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    gap: 0.5rem;
}

.product-description {
    width: 80vw;
    min-width: 300px;
    height: 200px;
    padding: 10px;
    border-radius: 1rem;
    resize: none;
}

.img-upload {
    width: 435px;
    height: 436px;
    border-radius: 1rem;
    border: 1px var(--main-black) solid;
    display: flex;
    align-items: center;
    justify-content: center;
}

.img-upload img {
    max-width: 100%;
    max-height: 100%;
    border-radius: 1rem;
}

.upload-file {
    display: flex;
    flex-direction: row;
    align-items: baseline;
    justify-content: flex-end;
    margin-top: 0.5rem;
    padding-right: 1rem;
    gap: 1rem;
    width: 100%;
    cursor: pointer;
}

.upload-file p {
    font-size: 15px;
    color: var(--main-black);
    font-weight: bold;
}

.upload-file p:hover {
    text-decoration: underline;
}

.upload-file img {
    height: 2rem;
}

.save-btn-container {
    display: flex;
    justify-content: flex-end;
    width: 100%;
}

.save-btn {
    padding: 0.5rem 3rem;
    background-color: var(--main-orange);
    --bs-btn-active-bg: var(--main-orange);
    margin-bottom: 2rem;
    margin-top: 2rem;
    margin-right: 2rem;
}

.save-btn:hover {
    background-color: var(--main-orange);
    opacity: 0.7;
}

.attributes-data {
    display: flex;
    flex-direction: row;
    gap: 1rem;
}

.add-attributes {
    color: var(--main-grey-separation);
    cursor: pointer;
}

.add-attributes:hover {
    opacity: 0.7;
}

.option-color {
    background-color: var(--main-grey-separation);
}
</style>