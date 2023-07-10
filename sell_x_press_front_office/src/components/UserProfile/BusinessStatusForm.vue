<template>
    <div :onChange="$emit('inputForm', formData)" class="form-container">
            <label for="input-company-number">COMPANY reference number</label>
            <input id="input-company-number" name="input-company-number" type="text" v-model="formData.username" />
        <div class="import-file-container">
            <div class="upload-file" @click="selectFile">
                <p>Import supporting file</p>
                <img src="../../assets/UserProfile/upload.png" alt="..." />
            </div>
            <input id="input-upload-file" name="input-upload-file" type="text" v-model="formData.upload_file" disabled/>
            <input id="file-input" type="file" style="display: none" @change="handleFileSelection" />
        </div>
    </div>
</template>
  
<script>
export default {
    name: 'UserProfileForm',
    data() {
        return {
            formData: {
                company_number: '',
                upload_file: '',
            },
        }
    },
    methods: {
        selectFile() {
            // Simuler un clic sur l'élément d'entrée de fichier invisible
            document.getElementById('file-input').click();
        },
        handleFileSelection(event) {
            const selectedFile = event.target.files[0];
            this.formData.upload_file = selectedFile.name;
            const inputElement = document.getElementById('input-upload-file');
            inputElement.style.width = inputElement.scrollWidth + 'px';
        }
    }
}
</script>
  
<style scoped>
.form-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 0.5rem;
    height: 75%;
    width: 100%;
    border-radius: 10px;
    padding: 0 3rem;
}

label {
    align-self: start;
    margin-left: 3vw;
    font-size: 15px;
    font-weight: bold;
}

input {
    border-radius: 15px;
    height: 53px;
    width: 90%;
    text-align: center;
    font-size: 20px;
    font-weight: bold;
}

.import-file-container {
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 1rem;
    margin-top: 2rem;
}

.upload-file {
    display: flex;
    flex-direction: row;
    align-items: baseline;
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

.input-upload-file {
    font-size: 0.5rem;
}
</style>